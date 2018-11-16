
## Create a Kubernetes Cluster

## Add Secrets

https://kubernetes.io/docs/concepts/configuration/secret/

```bash
kubectl create secret generic appsettings \
        --from-literal=CosmosDb__Endpoint=CosmosDb_Endpoint \
        --from-literal=CosmosDb__Key=CosmosDb_Key \
        --from-literal=AzureStorage__StorageAccountName=Storage_AccountName \
        --from-literal=AzureStorage__Key=Storage_AccountKey \
        --from-literal=ApplicationInsights__InstrumentationKey=ApplicationInsights_Key
```

## Deploy the Applications

```bash
kubectl create -f kubernetes.yml
```

## Additional Topics

- Static IP Address for services
