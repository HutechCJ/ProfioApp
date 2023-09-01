#!/bin/bash

grep -q "vm.max_map_count=262144" /etc/sysctl.conf

if [ $? -ne 0 ]; then
    echo "vm.max_map_count=262144" | sudo tee -a /etc/sysctl.conf
    sudo sysctl -w vm.max_map_count=262144
    echo "The value of vm.max_map_count has been set to 262144."
else
    echo "The value of vm.max_map_count is already 262144."
fi
