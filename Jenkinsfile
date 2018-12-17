def label = "worker-${UUID.randomUUID().toString()}"


podTemplate(label: label, containers: [
  containerTemplate(name: 'dockercompose', image: 'tmaier/docker-compose:latest', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'kubectl', image: 'lachlanevenson/k8s-kubectl:v1.8.8', command: 'cat', ttyEnabled: true),
  containerTemplate(name: 'helm', image: 'lachlanevenson/k8s-helm:latest', command: 'cat', ttyEnabled: true)
],
volumes: [
  hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock')
]) {
  node(label) {
    def myRepo = checkout scm
    def gitCommit = myRepo.GIT_COMMIT
	def shortGitCommit = "${gitCommit[0..10]}"
    def gitBranch = myRepo.GIT_BRANCH
    def dockerregistry = "development.nfwregistry.com"
	def helmchartrepository = "http://192.168.1.204:8080"
	def semanticVersion = "1.0.0-${gitBranch}.${shortGitCommit}"
	def name = "subjectmicroservice-${semanticVersion}.tgz"
	
	stage('Build Docker images') {
      container('dockercompose') {
          sh """
		    cd subjectmicroservice 
            docker-compose build
          """
      }
    }

	stage('Push Helm Chart') {
      container('helm') {
	   withCredentials([[$class: 'UsernamePasswordMultiBinding',
          credentialsId: 'dockerregistry',
          usernameVariable: 'DOCKER_USER',
          passwordVariable: 'DOCKER_PASSWORD']]) {
			  sh """
				helm init --client-only
				cd subjectmicroservice/subjectmicroservice/charts/subjectmicroservice
				sed -i.bak -e 's/TAG/${semanticVersion}/g' values.yaml
				helm dependency update
				cd ..
				helm package --app-version=${semanticVersion} --version=${semanticVersion} subjectmicroservice
				apk add curl
				curl --data-binary "@${name}" ${helmchartrepository}/api/charts
			  """
		  }
      }
    }

	stage('Push Docker image') {
      container('dockercompose') {
          withCredentials([[$class: 'UsernamePasswordMultiBinding',
          credentialsId: 'dockerregistry',
          usernameVariable: 'DOCKER_USER',
          passwordVariable: 'DOCKER_PASSWORD']]) {
          sh """
            docker login ${dockerregistry} -u ${DOCKER_USER} -p ${DOCKER_PASSWORD}
            docker tag subjectmicroservice:latest ${dockerregistry}/baltraining/subjectmicroservice:${semanticVersion} 
            docker push ${dockerregistry}/baltraining/subjectmicroservice:${semanticVersion}
            """
        }
      }
    }
	
	stage('Create Kubernetes namespace') {
	  try{
		  container('kubectl') {
			  sh """
				kubectl create namespace ${gitBranch}
			  """
		  }
	  }
      catch (exc) {
        println "Ignore"
      }
    }
  }
}