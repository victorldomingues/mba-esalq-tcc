docker_build(){
    file=$2
    if test -z $2 
    then
        file="app/src/$1/Dockerfile"
    else
        file="app/src/$1/$2/Dockerfile"
    fi
    echo docker build -t  "victordomingues/esalq-tcc-$1" --file $file  . $3 #--progress=plain --no-cache
    docker build -t  "victordomingues/esalq-tcc-$1" --file $file . $3 #--progress=plain --no-cache
    #docker login
    docker_image $1
    docker_push $1
}


docker_image(){
    echo "docker_image() victordomingues/esalq-tcc-$1  victordomingues/esalq-tcc-$1:latest"
    docker image tag "victordomingues/esalq-tcc-$1" "victordomingues/esalq-tcc-$1:latest"
}

docker_push(){
    echo "docker_push() victordomingues/esalq-tcc-$1:latest"
    docker push "victordomingues/esalq-tcc-$1:latest"
}

docker_build "api-autenticacao" "Api.Autenticacoes"
docker_build "api-saldo" "Api.Saldos"
docker_build "api-cadastro" "Api.Cadastro"
docker_build "api-movimentacoes" "Api.Movimentacoes"
docker_build "ui" ""  "--progress=plain --no-cache" 
