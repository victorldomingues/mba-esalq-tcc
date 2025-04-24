kubectl port-forward svc/argocd-server -n argocd 8000:443 
kubectl port-forward svc/esalq-postgres-service 5432:5432 
kubectl port-forward svc/redis  6379:6379 
kubectl port-forward svc/api-cadastro 6000:80 
