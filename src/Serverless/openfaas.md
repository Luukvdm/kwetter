https://github.com/openfaas/faas-netes/blob/master/chart/openfaas/README.md
https://faun.pub/getting-started-with-openfaas-on-minikube-634502c7acdf

https://www.openfaas.com/blog/asp-net-core/

```sh
kubectl apply -f https://raw.githubusercontent.com/openfaas/faas-netes/master/namespaces.yml
helm repo add openfaas https://openfaas.github.io/faas-netes/
helm repo update  && \
helm upgrade openfaas --install openfaas/openfaas \
    --namespace openfaas  \
    --set functionNamespace=openfaas-fn \
    --set generateBasicAuth=true
```

My password:

```sh
echo $(kubectl -n openfaas get secret basic-auth -o jsonpath="{.data.basic-auth-password}" | base64 --decode)
```

Problem:

```
NAME                READY   UP-TO-DATE   AVAILABLE   AGE
alertmanager        1/1     1            1           15m
basic-auth-plugin   1/1     1            1           15m
gateway             0/1     1            0           15m
nats                1/1     1            1           15m
prometheus          1/1     1            1           15m
queue-worker        0/1     1            0           15m
```

Troubleshoot

```sh
kubectl get deploy -n openfaas
kubectl logs -n openfaas deploy/gateway gateway
kubectl rollout restart -n openfaas deploy/gateway

kubectl get events -n openfaas --sort-by=.metadata.creationTimestamp
```

Solution:

```
????
```
