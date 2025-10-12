# Guide de démarrage

## Prérequis

- **.NET 8.0 SDK** - [Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0)
- **MongoDB** - [Télécharger](https://www.mongodb.com/try/download/community)
- **Redis** - [Télécharger](https://redis.io/download)
- **Visual Studio 2022** ou **VS Code** avec C# extension

## Installation rapide

### 1. Cloner le repository

```bash
git clone <repository-url>
cd alternance-backend
```

### 2. Démarrer MongoDB et Redis

#### Option A: Avec Docker

```bash
docker-compose up -d mongodb redis
```

#### Option B: Installation locale

**MongoDB**:
```bash
# Windows
net start MongoDB

# macOS/Linux
sudo systemctl start mongod
```

**Redis**:
```bash
# Windows
redis-server

# macOS
brew services start redis

# Linux
sudo systemctl start redis
```

### 3. Configurer les variables d'environnement

```bash
cp .env.example .env
```

Éditer le fichier `.env` selon votre configuration.

### 4. Restaurer les packages NuGet

```bash
dotnet restore
```

### 5. Compiler le projet

```bash
dotnet build
```

### 6. Lancer l'application

```bash
dotnet run --project src/Alternance.Api/Alternance.Api.csproj
```

L'API sera disponible sur:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger**: https://localhost:5001/swagger

## Premiers pas

### 1. Tester le Health Check

```bash
curl https://localhost:5001/health
```

Réponse attendue:
```json
{
  "status": "OK",
  "timestamp": "2024-01-01T12:00:00Z",
  "architecture": "Onion Architecture with CQRS"
}
```

### 2. Créer un utilisateur

```bash
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "email": "john.doe@example.com",
    "password": "SecurePassword123!",
    "firstName": "John",
    "lastName": "Doe",
    "userType": "Student"
  }'
```

### 3. Récupérer un utilisateur

```bash
curl https://localhost:5001/api/users/{userId}
```

## Structure du projet

```
alternance-backend/
├── src/
│   ├── Alternance.Api/              # API Layer
│   ├── Alternance.Application/      # Application Layer (CQRS)
│   ├── Alternance.Domain/           # Domain Layer (Business)
│   └── Alternance.Infrastructure/   # Infrastructure Layer
├── Alternance.sln                   # Solution file
├── README.md
├── ARCHITECTURE.md
├── GETTING_STARTED.md
└── docker-compose.yml
```

## Développement

### Avec Visual Studio 2022

1. Ouvrir `Alternance.sln`
2. Définir `Alternance.Api` comme projet de démarrage
3. Appuyer sur `F5` pour lancer en mode debug

### Avec VS Code

1. Ouvrir le dossier dans VS Code
2. Installer l'extension "C# Dev Kit"
3. Appuyer sur `F5` pour lancer en mode debug

### Avec .NET CLI

```bash
# Mode développement avec hot reload
dotnet watch run --project src/Alternance.Api/Alternance.Api.csproj

# Mode production
dotnet run --project src/Alternance.Api/Alternance.Api.csproj --configuration Release
```

## Tests

### Exécuter tous les tests

```bash
dotnet test
```

### Exécuter les tests avec couverture

```bash
dotnet test /p:CollectCoverage=true /p:CoverageReportFormat=opencover
```

## Commandes utiles

### Créer une nouvelle migration

```bash
dotnet ef migrations add MigrationName --project src/Alternance.Infrastructure
```

### Mettre à jour la base de données

```bash
dotnet ef database update --project src/Alternance.Infrastructure
```

### Nettoyer et recompiler

```bash
dotnet clean
dotnet build
```

### Publier pour production

```bash
dotnet publish -c Release -o ./publish
```

## Docker

### Build l'image

```bash
docker build -t alternance-backend .
```

### Lancer avec Docker Compose

```bash
docker-compose up
```

Cela va démarrer:
- L'API sur le port 5000
- MongoDB sur le port 27017
- Redis sur le port 6379

### Arrêter les containers

```bash
docker-compose down
```

## Dépannage

### Port déjà utilisé

Si le port 5000/5001 est déjà utilisé:

**Option 1**: Modifier dans `appsettings.json`
```json
{
  "Urls": "https://localhost:7001;http://localhost:7000"
}
```

**Option 2**: Utiliser une variable d'environnement
```bash
export ASPNETCORE_URLS="https://localhost:7001;http://localhost:7000"
```

### MongoDB ne démarre pas

Vérifier que MongoDB est bien installé et démarré:

```bash
# Windows
sc query MongoDB

# macOS/Linux
systemctl status mongod
```

### Redis ne démarre pas

```bash
# Windows
redis-cli ping

# macOS/Linux
redis-cli ping
```

Devrait retourner: `PONG`

### Erreur de certificat HTTPS

En développement, approuver le certificat de développement:

```bash
dotnet dev-certs https --trust
```

## Variables d'environnement importantes

| Variable | Description | Défaut |
|----------|-------------|--------|
| `ASPNETCORE_ENVIRONMENT` | Environnement (Development, Production) | Development |
| `ConnectionStrings__MongoDB` | Chaîne de connexion MongoDB | mongodb://localhost:27017 |
| `ConnectionStrings__Redis` | Chaîne de connexion Redis | localhost:6379 |
| `JwtSettings__Secret` | Clé secrète JWT | (à configurer) |

## Support

Pour toute question ou problème:
- Créer une issue sur GitHub
- Consulter la documentation dans `ARCHITECTURE.md`
- Vérifier les logs dans le dossier `logs/`

## Ressources

- [Documentation .NET](https://docs.microsoft.com/dotnet/)
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [MongoDB .NET Driver](https://mongodb.github.io/mongo-csharp-driver/)
- [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/)
