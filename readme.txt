docker pull redis

docker run -d -p 6379:6379 --name local-redis redis

docker ps

docker logs local-redis

--------------------------------------

docker exec -it local-redis sh
# redis-cli



---------------------------
Run the application

--------------------------------------

Open the POST MAN
Create the POST request. 
URL: https://localhost:44383/api/validate
BODY: JSON RAW
{
    "environment": "Dev",
    "jobid": "1"
}