.DEFAULT_GOAL := default
#################### PACKAGE ACTIONS ###################

stop_rabbitmq: 
				docker stop rabbitmq
				
run_docker:
				docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management                  

first_start: 
				rabbitmqadmin declare queue name=PokéQ durable=true
				rabbitmqadmin declare exchange name=PokéEx type=fanout
				rabbitmqadmin declare binding source=PokéEx destination=PokéQ destination_type=queue
				rabbitmqadmin declare exchange name=PokéQ type=fanout
				rabbitmqadmin declare binding source=PokéQ destination=PokéQ destination_type=queue


