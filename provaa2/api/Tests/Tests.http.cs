### Cadastrar usuário
POST https://localhost:5001/usuarios/cadastrar
Content-Type: application/json

{
  "email": "teste@example.com",
  "senha": "123456"
}

### Login
POST https://localhost:5001/usuarios/login
Content-Type: application/json

{
  "email": "teste@example.com",
  "senha": "123456"
}

### Listar usuários (protegido)
GET https://localhost:5001/usuarios/listar
Authorization: Bearer {{token}}

### Cadastrar evento (protegido)
POST https://localhost:5001/eventos/cadastrar
Authorization: Bearer {{token}}
Content-Type: application/json

{
  "nome": "Meu Evento",
  "local": "Local do Evento",
  "data": "2023-12-31T20:00:00"
}

### Listar todos os eventos
GET https://localhost:5001/eventos/listar

### Listar eventos do usuário (protegido)
GET https://localhost:5001/eventos/usuario
Authorization: Bearer {{token}}