
subir_app(){
    echo "create app=$1 path=app/infra/$1"
    argocd app create "$1" --repo https://github.com/victorldomingues/tcc-mba-esalq.git --path "app/infra/$1" --dest-server https://kubernetes.default.svc --dest-namespace default
}

subir_app ingress
subir_app redis
subir_app postgres
subir_app api-cadastro
subir_app api-autenticacao
subir_app api-saldo
subir_app api-movimentacoes
subir_app ui
subir_app datadog