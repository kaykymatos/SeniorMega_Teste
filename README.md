# Web_Api_Authentitacion 

## Swagger
  Nesta aplicação foi ultilizado o Swagger para efetuar as requests na API externa que esta sendo consumida com uma biblioteca chamada RestSharp que foi ultilizada neste projeto.

## Login
  O login é efetuado através desta request, nela é enviado um corpo contendo o login e a senha para o servidor que estará retornando uma resposta assim que o login e senha forem validados.  
  ![Login](https://user-images.githubusercontent.com/64444829/183306491-3d5908ce-617d-4253-9ecc-6b1744815231.PNG)

## Token de autenticação
Assim que o login for validado será retornado um token, com ele você vai conseguir efetuar as outras requisições pois sem ele você não estaria autenticado na API para poder continuar.
  ![LoginToken](https://user-images.githubusercontent.com/64444829/183306492-be1ed354-af67-46d2-a4bc-655fcee5c2d8.PNG)

## GetAll
Esta é a parte do GetAll onde você estará pegando todos os registros do banco de dados.
Para poder estar efetuando o GetAll você precisa colocar o token de verificação que foi informado assim que você efetuou o login
![GetAll](https://user-images.githubusercontent.com/64444829/183306487-1530294b-2c55-412d-bbe7-093c519778b7.PNG)

## GetAll sendo executado
Assim que você colocar o token de verificação e clicar para executar os registros serão mostrados a você em sua tela em formato Json:
![GetAllExecute](https://user-images.githubusercontent.com/64444829/183306488-d0d31d78-febf-414e-92df-bfba3d1a6772.PNG)


## GetByCode
Neste metodo sera enviado uma requisição iformando o código do registro que você gostaria de pegar, nesta requisição sera necessário o token de autenticação para poder continuar;
![GetUserByCode](https://user-images.githubusercontent.com/64444829/183306489-49fcb716-5a7d-4ebe-916d-64198a78361d.PNG)

## GetByCode sendo executado
Quando você executar sera retornar para você apenas um registro contendo as informações em Json do objeto.
![GetUserByCodeExecute](https://user-images.githubusercontent.com/64444829/183306490-29d87d1f-862b-462a-9e77-1b968c54c681.PNG)