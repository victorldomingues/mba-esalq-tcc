<!-- SUBIR CLUSTER -->

# Cluster Kubernetes

Instalar home brew

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

Instalar kubectl

```bash
brew install kubectl
```

no meu caso MacOS

```bash
brew install
```

Instalar kind

```bash
brew install kind
```

criar cluster 

```bash
kind create cluster --config=app/infra/01-cluster.yaml --name=esalq-cluster
```

## Subir cluster 

<small>

Créditos: `https://github.com/badtuxx/DescomplicandoArgoCD/blob/main/pt/src/day-1/README.md#descomplicando-argocd---day-1`

</small>

Criar namespace

```bash
kubectl create namespace argocd
```

Instalar o argo

```bash
kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml
```


Instalar agorcd CLI

```bash
brew install argocd
```

Autenticar argocd

```bash
kubectl get svc -n argocd
```

Fazer port forward

```bash
kubectl port-forward svc/argocd-server -n argocd 8000:443
```

Login no argocd

```bash
kubectl get secret argocd-initial-admin-secret -n argocd -o jsonpath="{.data.password}" | base64 -d`
```

```bash
argocd login localhost:8000
```

Adicionar repositorio 

```bash
argocd repo add https://github.com/victorldomingues/mba-esalq-tcc.git --name mba-esalq-tcc  --username not-used --password [TOKEN]
```

Informações do contexto

```bash
kubectl config current-context
```

```bash
argocd cluster add kind-esalq-cluster  --in-cluster
```
<br/>

<!-- TODO:COMO SUBIR APPS-->
# Como subir Apps via Argo CD

Subir todos de uma vez:

```bash
app/infra/apps-argocd.sh
```

ou

<details><summary>Subir manualmente </summary>


Subir Banco

```bash
argocd app create postgres --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/postgres --dest-server https://kubernetes.default.svc --dest-namespace default
```

```bash
kubectl create secret generic esalq-postgres-secret --from-literal=POSTGRES_PASSWORD=[DEFINIR_SENHA_POSTGRES]
```

Subir Redis

```bash
argocd app create redis --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/redis --dest-server https://kubernetes.default.svc --dest-namespace default
```

Subir api cadastro

```bash
argocd app create api-cadastro --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/api-cadastro --dest-server https://kubernetes.default.svc --dest-namespace default
```


  Subir api autenticacao

```bash
argocd app create api-autenticacao --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/api-autenticacao --dest-server https://kubernetes.default.svc --dest-namespace default
```

  Subir api saldo

```bash
argocd app create api-saldo --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/api-saldo --dest-server https://kubernetes.default.svc --dest-namespace default
```

  Subir api movimentacoes

```bash
argocd app create api-movimentacoes --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/api-movimentacoes --dest-server https://kubernetes.default.svc --dest-namespace default
```

  Subir ui

```bash
argocd app create ui --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/ui --dest-server https://kubernetes.default.svc --dest-namespace default
```


# INGRESS NGINX

```bash
argocd app create ingress-nginx --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/ingress --dest-server https://kubernetes.default.svc --dest-namespace default
```
<!--
ou

Criar Ingress Controlle NGINX

`kubectl apply -f app/infra/ingress/ingress.yaml` <!-- kubectl delete -f app/infra/ingress/ingress.yaml -->

</details>

<br/>

 
# DATADOG

## Instalação Agent

https://docs.datadoghq.com/containers/kubernetes/installation/?tab=datadogoperator

Instalação do Helm

```shell
brew install helm
```

Instalação

1. Instalar helm

2. Datadog Operator
   
```shell
helm repo add datadog https://helm.datadoghq.com
helm install datadog-operator datadog/datadog-operator
kubectl create secret generic datadog-secret --from-literal api-key=<DATADOG_API_KEY>
```

3. Configurar Agent
4. Deploy Agent

```shell 
argocd app create datadog --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/datadog --dest-server https://kubernetes.default.svc --dest-namespace default
```


## Instrumentar monitoramento banco

https://docs.datadoghq.com/database_monitoring/setup_postgres/selfhosted/?tab=kubernetes

<!---

# ELK

1: https://www.elastic.co/guide/en/cloud-on-k8s/current/k8s-upgrading-eck.html#k8s-upgrade-instructions
2: https://surajsoni3332.medium.com/setting-up-elk-stack-on-kubernetes-a-step-by-step-guide-227690eb57f4


`argocd app create elk --repo https://github.com/victorldomingues/mba-esalq-tcc.git --path app/infra/elk --dest-server https://kubernetes.default.svc --dest-namespace default`



`kubectl apply -f elasticsearch.yaml`

`kubectl apply -f kibana.yaml`

`kubectl apply -f filebeat.yaml`

```bash
kubectl apply -f logstash.yaml
kubectl apply -f logstash-config.yaml
```


usuario: `elastic`
senha: pegar com o comando `kubectl get secret quickstart-es-elastic-user -o=jsonpath='{.data.elastic}' | base64 --decode; echo;     `


-->

<!-- 

# Dynatrace

helm install dynatrace-operator oci://public.ecr.aws/dynatrace/dynatrace-operator \
--create-namespace \
--namespace dynatrace \
--atomic

 kubectl apply -f app/infra/dynatrace/dynakube.yaml

 helm repo add fluent https://fluent.github.io/helm-charts

 helm repo update

helm install fluent-bit fluent/fluent-bit -f app/infra/dynatrace/fluent-bit-values.yaml \
--create-namespace \
--namespace dynatrace-fluent-bit

-->

