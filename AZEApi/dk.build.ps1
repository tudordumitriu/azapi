param (
    [string]$version = 'localx',
	[string]$appName = 'azapi'
)

write-output "Building version: $version"

$Command = "docker build -t tudordumitriu/${appName}:$version ."
write-output "Docker: Executing $Command"
Invoke-Expression $Command

$Command = "docker push tudordumitriu/${appName}:$version"
write-output "Docker: Executing $Command"
Invoke-Expression $Command

write-output "K8S: Executing kubectl apply ..."
(Get-Content .\deployment.yaml).replace('{version}', $version) | Set-Content .\deploymentTemp.yaml
kubectl apply -f .\deploymentTemp.yaml

IF (-Not (kubectl rollout status deployment $appName)) {
    kubectl rollout undo deployment $appName
    kubectl rollout status deployment $appName    
}

write-output "Files: Deleting deploymentTemp.yaml"
Remove-Item –path .\deploymentTemp.yaml