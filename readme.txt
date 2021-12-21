docker pull redis

docker run -d -p 6379:6379 --name local-redis redis

docker ps

docker logs local-redis

--------------------------------------

docker exec -it local-redis sh
# redis-cli