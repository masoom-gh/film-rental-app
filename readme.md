						Film studion project   

****************************************************************************
Note: 
1. A collection of requests for running with Poastman is available in the 
project folder as a json file. You can import the collection via Postman and 
run the requests.

2. The database is seeded with one user (film studio) and one order. The username
is "smith@studio1.com" and the password is "Test2021!".
****************************************************************************

Endpoints:

1. Authentication
	i) /api/Authentication/Register => it is used for registering a film studio via a
	   POST action. 
	   Example of request body: 
	   {
		"username": "user@user.com",
		"Email": "user@user.com",
		"Password": "Test2021!",
		"StudioName": "test",
		"City": "Gutherborg",
		"ChairmanName": "Masoomeh",
		"ChairmanMobileNumber" : "073992299"
		}
	
	ii) /api/Authentication/Login => used for logging in via a POST method.
		Example of request body:
		{
		"username": "user@user.com",
		"Password": "Test2021!"
		}

	iii) /api/Authentication/RegisterAdmin => for registering as an admin (POST)
		Example of request body:
		{
		"username": "admin",
		"Email": "admin@admin.com",
		"Password": "Test2021!"
		}
		
	iv) /api/Authentication/logout => logout action which in fact does nothing because
		the JWT token cannot be removed. This request needs a token to run successfully.
		This is also a POST action.
		
		
2. Films
	i) /api/Films => for GETTING a list of films. No authorization is needed. However,
		if the user is authorized, a different response including more details about
		the films will be sent back.
		
	ii) /api/Films/{id} => for GETTING a film by id. No authorization is needed. However,
		if the user is authorized, a different response including more details about
		the films will be sent back.
		
	iii) /api/Films => POST method for creating a film. Authorization as "Admin" is 
		required.
		Example of request body:
		{
		"filmName": "new film",
		"releaseYear": 2021,
		"country": "test",
		"director": "test",
		"totalNumberOfCopies": 10
		}
	
	iV) /api/Films/{id} => PUT method for updaing film info. Authorization as "Admin" is 
		required. This method is not idempotent as it is now.
		Example of requets body:
		{
		"filmName": "Fast & Furious",
		"releaseYear": 2001,
		"country": "Sweden",
		"director": "Vin Diesel",
		"totalNumberOfCopies": 10,
		"ImageUrl" : "url"
		}
		
	v) /api/Films/{id} => DELETE method for removing a film from the database. Authorization as  
		"Admin" is required. This method is not idempotent as it is now. 
		
		
3. FilmStudios
	i) /api/Filmstudios => for GETTING a list of films studios. No authentication is required.
	
	ii) /api/FilmStudios/{username} => for GETTING a film studio by username. No authentication
     	is required.
		
		
4. RentalRecords 
	i) /api/RentalRecords => for GETTING a list of rental records. Authorization as  
		"Admin" is required. All rental by all films studios records are returned.
		
	ii) /api/RentalRecords => for booking a film. It is a POST method. Authorization as  
		"user" is required.
		Example of request body:
		{
		"film": {
			"filmid": 2
		},
		"filmstudio" :{
			"Username": "user@user.com"
				}
			}
			
	iii) /api/RentalRecords/{id} => for GETTING one rental record. Authorization as  
		"Admin" is required.
		
	iv) /api/RentalRecords/{id} => a DELETE method for removing a rental order or in other
		words returning a film. Authorization as "Admin" is required. 
		
	v) /api/RentalRecords/serach => for GETTING rental records for a logged in film studio.
		Authorization as "user" is required. If you wish to include the details of the
		booked movie, add the query parameter "includeOrderItem=true".
		
	