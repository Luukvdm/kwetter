gw=http://$(minikube ip):31112

faas-cli deploy -f ./stack.yml --gateway $gw

