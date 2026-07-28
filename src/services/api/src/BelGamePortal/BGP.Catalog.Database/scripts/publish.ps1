$containerName = "sqitch"
$workingDirectory = "/app"
$command = "sqitch status db:pg://sa@192.168.1.10/catalog --verbose"
$command = "psql -h 192.168.1.10 -U sa -d catalog -f /app/src/Master.sql"

# docker exec -it -w $workingDirectory $containerName bash -c "$command";
docker exec --user root -it -w $workingDirectory $containerName bash -c "$command"