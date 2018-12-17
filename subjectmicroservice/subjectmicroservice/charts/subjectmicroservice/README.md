# Subject 

This Microservice is responsible for NeoFace Watch Subjects and was created to separate and isolate subject management. It has a local database which stores the subjects data using redis.

```bash
helm install subjectmicroservice nfwrepo/subjectmicroservice --version=[semanticVersion] --namespace=[Branch] --set image.tag=[Version] --set imageCredentials.username=[DOCKER_USER] --set imageCredentials.password=[DOCKER_PASSWORD]
```

## Introduction

This chart bootstraps a Subject Microservice deployment on a [Kubernetes](http://kubernetes.io) cluster using the [Helm](https://helm.sh) package manager.

It also packages the [RabbitMQ](https://github.com/helm/charts/tree/master/stable/rabbitmq) which is required for bootstrapping a RabbitMQ deployment for the message bus requirements of the Subject microservice.

## Prerequisites

- Kubernetes 1.8+ (tested with Azure Kubernetes Service, Google Kubernetes Engine, minikube and Docker for Desktop Kubernetes)
- Helm 2.10.0+
- Administrative access to the cluster to create and update RBAC ClusterRoles

## Installing the Chart

To install the chart with the release name `kubeapps`:

```console
helm repo add nfwrepo http://192.168.1.204:8080
helm install subjectmicroservice nfwrepo/subjectmicroservice --version=[semanticVersion] --namespace=[Branch] --set image.tag=[Version] --set imageCredentials.username=[DOCKER_USER] --set imageCredentials.password=[DOCKER_PASSWORD]
```

## Uninstalling Subject Microservice

```console
$ helm delete --purge subjectmicroservice
```


## Configuration

For a full list of configuration parameters of the Subject Microservice chart, see the [values.yaml](values.yaml) file.

Specify each parameter using the `--set key=value[,key=value]` argument to `helm install`. For example,

```console
$ helm install subjectmicroservice nfwrepo/subjectmicroservice \
  --version=[semanticVersion] --namespace=[Branch] \
  --set image.tag=[Version] --set imageCredentials.username=[DOCKER_USER] \
  --set imageCredentials.password=[DOCKER_PASSWORD] \
  --set service.type=NodePort 
```

The above command sets the service type to NodePort 

Alternatively, a YAML file that specifies the values for parameters can be provided while installing the chart. For example,

```console
$ helm install subjectmicroservice nfwrepo/subjectmicroservice \
  -f custom-values.yaml
```
