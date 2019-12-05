docker build -t tudordumitriu/azapi:v6 .
docker push tudordumitriu/azapi:v6
kubectl apply -f .\deployment.yaml
kubectl get svc -n azapi
