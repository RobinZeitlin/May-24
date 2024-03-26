#!/bin/bash
platforms=( StandaloneWindows64 StandaloneWindows StandaloneOSX StandaloneLinux64)

json='{"include" : []}'
for ((i=1; i<=${#platforms[@]}; i++)); do
   if [[ ${!i} == "true" ]]; then
	   chosenplatforms+=("${platforms[i-1]}")
      depotid=$(($i + $5))
	   json+=$(echo "$json" | jq --arg platform "${platforms[i-1]}" --arg depotid "$depotid" '.include += [{"targetPlatform" : $platform, "depotid" : $depotid}]')
   fi
done
echo "$json" | jq -sc "add"
