.DEFAULT_GOAL := default
#################### PACKAGE ACTIONS ###################

stop_rabbitmq: 
				docker stop rabbitmq
run_message:
				cd send && dotnet run
				cd receive && dotnet run 
				cd ..
				
run_docker:
				docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management                  


