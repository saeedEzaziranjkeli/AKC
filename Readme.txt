AKC Challenge

I used :
.Net Core 3.1
Azure Sql Server
Newtonsoft 
OAuth2 and JWT Token
CQRS Model with MediatR
DDD design pattern
EFCore Code First

First you should register the user and then login -> you get JWT-Token and then for each request to DrugController please add this Token to your Authorization 
header wirh Bearer


Please add your Sql Server ConnectionString in Appsettings.Development.json, I did develop AutoMigrate, if there is no table, it is generate automaticly

I pushed this code in my repository and this project has Public Access: 
	URL : https://github.com/saeedEzaziranjkeli/AKC.git

For any question: please send me an Email : Saeed.ezazy@gmail.com