# SelfHost
SelfHost service with .net6 for Raion with two endPoints

After running client one should be able to connect via losalhost:7560

To see existing records go to https://localhost:7260

To post result use swagger https://localhost:7260/SaveToFile

Example:
curl -X 'POST' \
  'https://localhost:7260/SaveToFile?receivedValue=asfasf' \
  -H 'accept: */*' \
  -d ''
  
Inserted data will be added to textfile called receivedValues.txt
