# DeliVeggieApp

To run this application please do the followings:

1. Run rabbitmq on your local e.g. in docker desktop.
2. Run this console application Microservices => ProductMicroservice which will act as consumer.
3. Run this .net core web api Gateway => ProductService which serve the requests for the UI application.
4. Then run the angular application DeliVeggieUIApp 
              To configure the web api for the UI application please change the apiUrl in environment.ts file.
              e.g. apiUrl: 'http://localhost:portnumber/api/'
