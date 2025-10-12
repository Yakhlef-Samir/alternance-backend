# Alternance Backend - C# .NET

Backend API pour plateforme d'alternance utilisant **Onion Architecture** avec **CQRS** en C#.

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    API Layer (ASP.NET Core)                  │
│  Controllers, Middlewares, Authentication, Rate Limiting     │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│              Application Layer (CQRS avec MediatR)           │
│  Commands, Queries, Handlers, DTOs                           │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│              Domain Layer (Business Logic)                   │
│  Entities, Business Rules, Domain Events                     │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│              Infrastructure Layer                            │
│  MongoDB, Redis, Email, Storage, AI Services                 │
└─────────────────────────────────────────────────────────────┘
```

## Structure du Projet

```
alternance-backend/
├── src/
│   ├── Alternance.Api/              # API Layer
│   │   ├── Controllers/             # Contrôleurs REST
│   │   ├── Middlewares/             # Middlewares personnalisés
│   │   └── Program.cs               # Point d'entrée
│   │
│   ├── Alternance.Application/      # Application Layer
│   │   ├── Commands/                # Commandes CQRS (Write)
│   │   ├── Queries/                 # Requêtes CQRS (Read)
│   │   ├── Handlers/                # Handlers pour Commands & Queries
│   │   └── DTOs/                    # Data Transfer Objects
│   │
│   ├── Alternance.Domain/           # Domain Layer
│   │   ├── Entities/                # Entités du domaine
│   │   ├── Business/                # Logique métier
│   │   └── Events/                  # Événements du domaine
│   │
│   └── Alternance.Infrastructure/   # Infrastructure Layer
│       ├── Repositories/            # Accès aux données (MongoDB)
│       ├── Cache/                   # Cache distribué (Redis)
│       └── Services/                # Services externes
│
└── .env.example                     # Variables d'environnement
```

## Technologies

- **.NET 8.0** - Framework
- **ASP.NET Core** - API Web
- **MediatR** - Pattern CQRS
- **MongoDB** - Base de données NoSQL
- **Redis** - Cache distribué
- **FluentValidation** - Validation
- **JWT** - Authentification

## Prérequis

- .NET 8.0 SDK
- MongoDB
- Redis
- Visual Studio 2022 ou VS Code

## Installation

1. Cloner le repository
```bash
git clone <repository-url>
cd alternance-backend
```

2. Restaurer les packages NuGet
```bash
dotnet restore
```

3. Configurer les variables d'environnement
```bash
cp .env.example .env
# Éditer le fichier .env avec vos configurations
```

4. Lancer l'application
```bash
dotnet run --project src/Alternance.Api
```

L'API sera disponible sur `https://localhost:5001`

## Swagger/OpenAPI

Documentation API interactive disponible sur:
- Development: `https://localhost:5001/swagger`

## Endpoints

### Health Check
```
GET /health
```

### Users
```
POST /api/users              # Créer un utilisateur
GET  /api/users/{id}         # Récupérer un utilisateur
PUT  /api/users/{id}         # Mettre à jour le profil
```

### Jobs
```
POST /api/jobs               # Créer une offre
GET  /api/jobs               # Lister les offres
GET  /api/jobs/{id}          # Récupérer une offre
```

### Applications
```
POST /api/applications       # Postuler à une offre
GET  /api/applications       # Lister les candidatures
```

## Pattern CQRS

### Commands (Write Operations)
- `CreateUserCommand`
- `ApplyToJobCommand`
- `UpdateProfileCommand`
- `CreateJobCommand`

### Queries (Read Operations)
- `GetUserByIdQuery`
- `ListJobsQuery`
- `GetApplicationsQuery`
- `SearchCandidatesQuery`

## Principes de l'Architecture en Oignon

### 1. API Layer
- Point d'entrée HTTP
- Gestion des middlewares
- Validation des entrées
- Authentification/Autorisation

### 2. Application Layer
- Orchestration avec MediatR
- Commands et Queries
- Pas de logique métier
- Transformation des données (DTOs)

### 3. Domain Layer
- **Cœur de l'application**
- Logique métier pure
- Entités riches
- Événements du domaine
- **AUCUNE dépendance externe**

### 4. Infrastructure Layer
- Implémentation technique
- Accès aux données
- Services externes
- Cache, Email, Storage, AI

## Tests

```bash
dotnet test
```

## Build Production

```bash
dotnet publish -c Release -o ./publish
```

## Docker

```bash
docker build -t alternance-backend .
docker run -p 5000:80 alternance-backend
```

## Contribuer

1. Fork le projet
2. Créer une branche feature (`git checkout -b feature/AmazingFeature`)
3. Commit les changements (`git commit -m 'Add AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

## Licence

MIT
# alternance-backend
