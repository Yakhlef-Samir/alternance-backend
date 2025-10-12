# Architecture détaillée

## Onion Architecture avec CQRS

### Vue d'ensemble

L'architecture en oignon (Onion Architecture) est une architecture logicielle qui organise le code en couches concentriques, avec le domaine métier au centre.

```
┌─────────────────────────────────────────────────────────────┐
│                        API Layer (HTTP)                      │
│  ┌───────────────────────────────────────────────────────┐  │
│  │            Controllers & Middlewares                   │  │
│  │  - Authentication, Validation, Rate Limiting, Cache   │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer (CQRS)                 │
│  ┌─────────────────────┐      ┌─────────────────────────┐  │
│  │      Commands       │      │        Queries          │  │
│  │  - CreateUser       │      │  - GetUserById          │  │
│  │  - ApplyToJob       │      │  - ListJobs             │  │
│  │  - UpdateProfile    │      │  - GetApplications      │  │
│  │  - CreateJob        │      │  - SearchCandidates     │  │
│  └─────────────────────┘      └─────────────────────────┘  │
│            ↓                              ↓                  │
│  ┌─────────────────────┐      ┌─────────────────────────┐  │
│  │  Command Handlers   │      │    Query Handlers       │  │
│  └─────────────────────┘      └─────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                      Domain Layer (Core)                     │
│  ┌───────────────────────────────────────────────────────┐  │
│  │              Domain Entities & Models                  │  │
│  │  - User, Student, Company, Job, Application           │  │
│  │                                                        │  │
│  │              Business Logic & Rules                    │  │
│  │  - ApplicationWorkflow, MatchingAlgorithm             │  │
│  │                                                        │  │
│  │              Domain Events                             │  │
│  │  - UserCreated, ApplicationSubmitted, JobPublished    │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                   Infrastructure Layer                       │
│  ┌─────────────┐  ┌──────────┐  ┌────────────────────────┐ │
│  │ Repositories│  │  Cache   │  │  External Services     │ │
│  │  - MongoDB  │  │  - Redis │  │  - Email, Storage, AI  │ │
│  └─────────────┘  └──────────┘  └────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

## Couches

### 1. Domain Layer (Cœur)

**Emplacement**: `Alternance.Domain`

**Responsabilités**:
- Définir les entités du domaine
- Implémenter la logique métier
- Définir les événements du domaine
- **AUCUNE dépendance externe**

**Contenu**:
- `Entities/` - Entités riches avec comportements
  - `User.cs`
  - `Student.cs`
  - `Company.cs`
  - `Job.cs`
  - `Application.cs`
- `Business/` - Règles métier complexes
  - `ApplicationWorkflow.cs`
  - `MatchingAlgorithm.cs`
- `Events/` - Événements du domaine
  - `UserCreatedEvent.cs`
  - `ApplicationSubmittedEvent.cs`
  - `JobPublishedEvent.cs`

**Principe**: Cette couche ne doit **JAMAIS** dépendre des autres couches.

### 2. Application Layer (Orchestration)

**Emplacement**: `Alternance.Application`

**Responsabilités**:
- Orchestrer les cas d'utilisation
- Implémenter CQRS avec MediatR
- Transformer les données (DTOs)
- Coordination entre Domain et Infrastructure

**Contenu**:
- `Commands/` - Opérations d'écriture
  - `CreateUserCommand.cs`
  - `ApplyToJobCommand.cs`
  - `UpdateProfileCommand.cs`
  - `CreateJobCommand.cs`
- `Queries/` - Opérations de lecture
  - `GetUserByIdQuery.cs`
  - `ListJobsQuery.cs`
  - `GetApplicationsQuery.cs`
  - `SearchCandidatesQuery.cs`
- `Handlers/` - Handlers MediatR
  - `CreateUserCommandHandler.cs`
  - `GetUserByIdQueryHandler.cs`
- `DTOs/` - Data Transfer Objects
  - `UserDto.cs`
  - `JobDto.cs`
  - `ApplicationDto.cs`

**Pattern CQRS**:
- **Commands**: Modifient l'état, retournent un ID ou boolean
- **Queries**: Lecture seule, retournent des DTOs

### 3. Infrastructure Layer (Implémentation Technique)

**Emplacement**: `Alternance.Infrastructure`

**Responsabilités**:
- Implémentation de la persistance
- Accès aux services externes
- Gestion du cache
- Configuration technique

**Contenu**:
- `Repositories/` - Accès aux données
  - `IRepository.cs` - Interface générique
  - `MongoRepository.cs` - Implémentation MongoDB
  - `MongoDbContext.cs` - Contexte MongoDB
- `Cache/` - Cache distribué
  - `IRedisCache.cs`
  - `RedisCache.cs`
- `Services/` - Services externes
  - `EmailService.cs` - Envoi d'emails
  - `StorageService.cs` - Stockage de fichiers (S3, Azure)
  - `AIService.cs` - Services d'IA (OpenAI)

**Technologies**:
- MongoDB.Driver
- StackExchange.Redis
- Services externes (SMTP, S3, OpenAI)

### 4. API Layer (Présentation)

**Emplacement**: `Alternance.Api`

**Responsabilités**:
- Exposer les endpoints HTTP
- Gérer l'authentification/autorisation
- Validation des entrées
- Rate limiting et cache HTTP

**Contenu**:
- `Controllers/` - Contrôleurs REST
  - `BaseController.cs` - Contrôleur de base
  - `UsersController.cs`
  - `JobsController.cs`
  - `ApplicationsController.cs`
- `Middlewares/` - Middlewares personnalisés
  - `AuthenticationMiddleware.cs`
  - `ValidationMiddleware.cs`
  - `RateLimitMiddleware.cs`
  - `CacheMiddleware.cs`
- `Program.cs` - Configuration et démarrage

**Technologies**:
- ASP.NET Core
- JWT Bearer Authentication
- FluentValidation
- Swagger/OpenAPI

## Flux de données

### Commande (Write)

```
Client → Controller → MediatR → CommandHandler → Domain → Infrastructure → Database
```

**Exemple**: Créer un utilisateur
1. Client envoie POST `/api/users`
2. Controller reçoit et valide
3. MediatR route vers `CreateUserCommandHandler`
4. Handler crée une entité `User` (Domain)
5. Repository sauvegarde dans MongoDB
6. Retourne l'ID du nouvel utilisateur

### Requête (Read)

```
Client → Controller → MediatR → QueryHandler → Infrastructure → Database
```

**Exemple**: Récupérer un utilisateur
1. Client envoie GET `/api/users/{id}`
2. Controller reçoit
3. MediatR route vers `GetUserByIdQueryHandler`
4. Handler interroge le repository
5. Repository récupère de MongoDB
6. Transforme en `UserDto`
7. Retourne au client

## Avantages de cette architecture

### 1. Séparation des responsabilités
Chaque couche a un rôle clairement défini.

### 2. Testabilité
- Domain Layer: Testable sans infrastructure
- Application Layer: Tests avec mocks
- Infrastructure Layer: Tests d'intégration

### 3. Maintenabilité
- Changements isolés par couche
- Évolution facile

### 4. Indépendance technologique
- Le Domain ne dépend de rien
- Changement de DB sans impact sur le domaine

### 5. CQRS
- Optimisation séparée des lectures/écritures
- Scalabilité horizontale possible
- Séparation des modèles de lecture et d'écriture

## Dépendances entre couches

```
Api → Application → Domain
 ↓         ↓
Infrastructure
```

**Règle d'or**: Les dépendances vont toujours vers le centre (Domain).

## Pattern de conception utilisés

1. **Onion Architecture** - Organisation en couches
2. **CQRS** - Séparation Command/Query
3. **Repository Pattern** - Abstraction d'accès aux données
4. **Mediator Pattern** - MediatR pour découplage
5. **Dependency Injection** - IoC Container ASP.NET Core
6. **Domain Events** - Communication asynchrone

## Bonnes pratiques

### Domain Layer
- Logique métier pure
- Pas de dépendances externes
- Entités riches avec comportements
- Validation métier dans les entités

### Application Layer
- Pas de logique métier
- Orchestration uniquement
- DTOs pour les transferts
- Validation des entrées

### Infrastructure Layer
- Implémentations techniques
- Gestion des transactions
- Resilience (retry, circuit breaker)

### API Layer
- Controllers légers
- Délégation à MediatR
- Gestion des erreurs HTTP
- Documentation OpenAPI
