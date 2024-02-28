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

first_start: 
				rabbitmqadmin declare queue name=PokéQ durable=true
				rabbitmqadmin declare exchange name=PokéEx type=fanout
				rabbitmqadmin declare binding source=PokéEx destination=PokéQ destination_type=queue
				rabbitmqadmin declare exchange name=PokéQ type=fanout
				rabbitmqadmin declare binding source=PokéQ destination=PokéQ destination_type=queue
				
install_package: 
				dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.1.21
				dotnet add package NServiceBus --version 7.8.4
				dotnet add package NServiceBus.Extensions.Hosting --version 1.1.0
				dotnet add package NServiceBus.Newtonsoft.Json --version 2.4.0
				dotnet add package NServiceBus.RabbitMQ --version 6.1.4
				dotnet add package RabbitMQ.Client --version 6.8.1








