#!/bin/bash

NAMESPACE="kwetter"

kubectl --namespace=$NAMESPACE apply -f base.yaml | grep -v "unchanged"

for f in ./*.yaml
do
	kubectl --namespace=$NAMESPACE apply -f $f | grep -v "unchanged"
done
