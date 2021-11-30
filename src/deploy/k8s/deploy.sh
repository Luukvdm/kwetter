#!/bin/bash

NAMESPACE="kwetter"

for f in ./*.yaml
do
	kubectl --namespace=$NAMESPACE apply -f $f | grep -v "unchanged"
done
