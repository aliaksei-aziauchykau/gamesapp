$containerName = "sqitch"
$workingDirectory = "/app"
$command = "sqitch status db:pg://sa@192.168.1.10/catalog --verbose"
$command = "sqitch deploy db:pg://sa@192.168.1.10/catalog --verbose"

# docker exec -it -w $workingDirectory $containerName bash -c "$command";
docker exec --user root -it -w $workingDirectory  $containerName bash 