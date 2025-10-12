# Structure complète du projet

```
alternance-backend/
│
├── src/
│   │
│   ├── Alternance.Api/                          # 🌐 API Layer (Présentation)
│   │   ├── Controllers/
│   │   │   └── BaseController.cs                # Contrôleur de base avec MediatR
│   │   │
│   │   ├── Middlewares/
│   │   │   ├── AuthenticationMiddleware.cs      # Middleware d'authentification
│   │   │   ├── ValidationMiddleware.cs          # Middleware de validation
│   │   │   ├── RateLimitMiddleware.cs          # Limitation du taux de requêtes
│   │   │   └── CacheMiddleware.cs              # Mise en cache HTTP
│   │   │
│   │   ├── Properties/
│   │   │   └── launchSettings.json             # Configuration de lancement
│   │   │
│   │   ├── Alternance.Api.csproj               # Fichier projet API
│   │   ├── Program.cs                           # Point d'entrée de l'application
│   │   └── appsettings.json                    # Configuration de l'application
│   │
│   ├── Alternance.Application/                  # 🎯 Application Layer (CQRS)
│   │   ├── Commands/                            # Commandes (Write Operations)
│   │   │   ├── CreateUserCommand.cs            # Créer un utilisateur
│   │   │   ├── ApplyToJobCommand.cs            # Postuler à une offre
│   │   │   ├── UpdateProfileCommand.cs         # Mettre à jour le profil
│   │   │   └── CreateJobCommand.cs             # Créer une offre d'emploi
│   │   │
│   │   ├── Queries/                             # Requêtes (Read Operations)
│   │   │   ├── GetUserByIdQuery.cs             # Récupérer un utilisateur
│   │   │   ├── ListJobsQuery.cs                # Lister les offres
│   │   │   ├── GetApplicationsQuery.cs         # Récupérer les candidatures
│   │   │   └── SearchCandidatesQuery.cs        # Rechercher des candidats
│   │   │
│   │   ├── Handlers/                            # Handlers MediatR
│   │   │   ├── CreateUserCommandHandler.cs     # Handler pour CreateUser
│   │   │   └── GetUserByIdQueryHandler.cs      # Handler pour GetUserById
│   │   │
│   │   ├── DTOs/                                # Data Transfer Objects
│   │   │   ├── UserDto.cs                      # DTO Utilisateur
│   │   │   ├── JobDto.cs                       # DTO Offre d'emploi
│   │   │   ├── ApplicationDto.cs               # DTO Candidature
│   │   │   └── StudentDto.cs                   # DTO Étudiant
│   │   │
│   │   ├── Alternance.Application.csproj       # Fichier projet Application
│   │   └── DependencyInjection.cs              # Configuration DI pour Application
│   │
│   ├── Alternance.Domain/                       # 💎 Domain Layer (Cœur Métier)
│   │   ├── Entities/                            # Entités du domaine
│   │   │   ├── User.cs                         # Entité Utilisateur
│   │   │   ├── Student.cs                      # Entité Étudiant
│   │   │   ├── Company.cs                      # Entité Entreprise
│   │   │   ├── Job.cs                          # Entité Offre d'emploi
│   │   │   └── Application.cs                  # Entité Candidature
│   │   │
│   │   ├── Business/                            # Logique métier
│   │   │   ├── ApplicationWorkflow.cs          # Workflow des candidatures
│   │   │   └── MatchingAlgorithm.cs           # Algorithme de matching
│   │   │
│   │   ├── Events/                              # Événements du domaine
│   │   │   ├── UserCreatedEvent.cs             # Événement: Utilisateur créé
│   │   │   ├── ApplicationSubmittedEvent.cs    # Événement: Candidature soumise
│   │   │   └── JobPublishedEvent.cs            # Événement: Offre publiée
│   │   │
│   │   ├── Interfaces/
│   │   │   └── IEntity.cs                      # Interface pour les entités
│   │   │
│   │   ├── Common/
│   │   │   └── BaseEntity.cs                   # Classe de base pour entités
│   │   │
│   │   └── Alternance.Domain.csproj            # Fichier projet Domain
│   │
│   └── Alternance.Infrastructure/               # 🔧 Infrastructure Layer
│       ├── Repositories/                        # Accès aux données
│       │   ├── IRepository.cs                  # Interface repository générique
│       │   ├── MongoRepository.cs              # Implémentation MongoDB
│       │   └── MongoDbContext.cs               # Contexte MongoDB
│       │
│       ├── Cache/                               # Système de cache
│       │   ├── IRedisCache.cs                  # Interface cache Redis
│       │   └── RedisCache.cs                   # Implémentation Redis
│       │
│       ├── Services/                            # Services externes
│       │   ├── IEmailService.cs                # Interface service email
│       │   ├── EmailService.cs                 # Implémentation email
│       │   ├── IStorageService.cs              # Interface stockage
│       │   ├── StorageService.cs               # Implémentation stockage (S3/Azure)
│       │   ├── IAIService.cs                   # Interface service IA
│       │   └── AIService.cs                    # Implémentation IA (OpenAI)
│       │
│       ├── Alternance.Infrastructure.csproj    # Fichier projet Infrastructure
│       └── DependencyInjection.cs              # Configuration DI pour Infrastructure
│
├── Alternance.sln                               # 📦 Solution Visual Studio
│
├── .env.example                                 # 🔐 Variables d'environnement (exemple)
├── .gitignore                                   # Git ignore
├── global.json                                  # Configuration SDK .NET
│
├── Dockerfile                                   # 🐳 Configuration Docker
├── .dockerignore                                # Docker ignore
├── docker-compose.yml                           # Orchestration Docker
│
├── README.md                                    # 📖 Documentation principale
├── ARCHITECTURE.md                              # 🏗️ Documentation architecture
├── GETTING_STARTED.md                           # 🚀 Guide de démarrage
└── PROJECT_STRUCTURE.md                         # 📁 Ce fichier - Structure du projet
```

## Légende

| Icône | Signification |
|-------|---------------|
| 🌐 | **API Layer** - Interface HTTP avec le monde extérieur |
| 🎯 | **Application Layer** - CQRS et orchestration |
| 💎 | **Domain Layer** - Cœur métier, logique business |
| 🔧 | **Infrastructure Layer** - Accès données et services externes |
| 📦 | Fichier solution |
| 🔐 | Configuration sensible |
| 🐳 | Docker |
| 📖 | Documentation |

## Dépendances entre les projets

```
Alternance.Api
    ├── → Alternance.Application
    │       └── → Alternance.Domain
    └── → Alternance.Infrastructure
            └── → Alternance.Domain
```

**Note importante**: Le projet `Alternance.Domain` ne dépend d'**AUCUN** autre projet.

## Technologies par couche

### API Layer (Alternance.Api)
- ASP.NET Core 8.0
- MediatR
- JWT Bearer Authentication
- FluentValidation
- Swagger/OpenAPI
- AspNetCoreRateLimit

### Application Layer (Alternance.Application)
- MediatR (CQRS)
- FluentValidation
- AutoMapper

### Domain Layer (Alternance.Domain)
- **AUCUNE dépendance externe**
- C# pur avec .NET 8.0

### Infrastructure Layer (Alternance.Infrastructure)
- MongoDB.Driver
- StackExchange.Redis
- Services externes (Email, Storage, AI)

## Nombre de fichiers créés

- **Total**: ~55 fichiers
- **Code C#**: ~35 fichiers
- **Configuration**: ~10 fichiers
- **Documentation**: ~5 fichiers
- **Docker**: ~3 fichiers
- **Autres**: ~2 fichiers

## Prochaines étapes recommandées

1. ✅ Structure créée
2. ⏭️ Implémenter les Controllers dans `Alternance.Api/Controllers/`
3. ⏭️ Compléter les Handlers dans `Alternance.Application/Handlers/`
4. ⏭️ Ajouter les tests unitaires et d'intégration
5. ⏭️ Configurer CI/CD
6. ⏭️ Déployer sur Azure/AWS

## Commandes rapides

```bash
# Restaurer les dépendances
dotnet restore

# Compiler
dotnet build

# Lancer l'API
dotnet run --project src/Alternance.Api

# Lancer avec Docker
docker-compose up
```
