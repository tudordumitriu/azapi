param (
    [string]$version = 'v10'
)

write-output "Building version: $version"

$Command = "docker build -t tudordumitriu/azapi:$version ."
write-output "Executing $Command"
Invoke-Expression $Command

$Command = "docker push tudordumitriu/azapi:$version"
write-output "Executing $Command"
Invoke-Expression $Command

write-output "Executing kubectl apply ..."
(Get-Content .\deployment.yaml).replace('{version}', $version) | Set-Content .\deploymentTemp.yaml
kubectl apply -f .\deploymentTemp.yaml
Remove-Item –path .\deploymentTemp.yaml


