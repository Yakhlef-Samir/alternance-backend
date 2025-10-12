using Alternance.Domain.Entities;
using Alternance.Domain.Enum;
using Alternance.Infrastructure.MongoDb;
using Alternance.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Alternance.Infrastructure.IntegrationTests;

public class MongoRepositoryTests
{
    private readonly JobsRepository _jobRepository;
    private readonly UsersRepository _userRepository;
    private readonly CompaniesRepository _companyRepository;
    private readonly StudentsRepository _studentRepository;
    private readonly ApplicationsRepository _applicationRepository;
    private static readonly string[] expected = ["C#", "MongoDB"];

    public MongoRepositoryTests()
    {
        // Use your real database connection string
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                // Change to your local database - use a test-specific DB to avoid affecting production data
                { "ConnectionStrings:MongoDB", "mongodb://localhost:27017/alternance-test-db" }
            })
            .Build();

        MongoDbContext context = new MongoDbContext(configuration);
        CollectionsIndex collectionsIndex = new CollectionsIndex(context);
        _jobRepository = collectionsIndex.Jobs;
        _userRepository = collectionsIndex.Users;
        _companyRepository = collectionsIndex.Companies;
        _studentRepository = collectionsIndex.Students;
        _applicationRepository = collectionsIndex.Applications;
    }
    #region Job data
    [Fact]
    public async Task AddAsync_ShouldInsertJobDocument()
    {
        Job job = new(
            jobId: Guid.NewGuid(),
            companyId: Guid.NewGuid(),
            title: "Développeur .NET",
            description: "Développement d'API REST avec ASP.NET Core.",
            location: "Paris",
            contractType: ContractType.Alternance,
            salary: 32000m);

        job.AddRequiredSkill("C#");
        job.AddRequiredSkill("MongoDB");

        await _jobRepository.AddAsync(job);

        List<Job> storedJobs = await _jobRepository.GetAllAsync();

        storedJobs
            .Should()
            .ContainSingle(j => j.Id == job.Id && j.Title == job.Title)
            .Which.RequiredSkills.Should().Contain(expected);
    }
    #endregion

    #region User data
    
    [Fact]
    public async Task AddAsync_ShouldCreateStudentUser()
    {
        // Scénario 1: Un étudiant s'inscrit sur la plateforme
        User studentUser = new User(
            userId: Guid.NewGuid(),
            email: "samir.yakhlef@student.com",
            passwordHash: "$2a$11$hashed_password_here", // BCrypt hash
            firstName: "Samir",
            lastName: "Yakhlef",
            userType: UserType.Student
        );

        await _userRepository.AddAsync(studentUser);

        List<User> storedUsers = await _userRepository.GetAllAsync();

        storedUsers
            .Should()
            .ContainSingle(u => u.Id == studentUser.Id && u.Email == studentUser.Email)
            .Which.Should().Match<User>(u =>
                u.UserType == UserType.Student &&
                u.FirstName == "Samir" &&
                u.LastName == "Yakhlef");
    }

    [Fact]
    public async Task AddAsync_ShouldCreateCompanyUser()
    {
        // Scénario 2: Une entreprise s'inscrit sur la plateforme
        User companyUser = new User(
            userId: Guid.NewGuid(),
            email: "contact@techcorp.com",
            passwordHash: "$2a$11$hashed_password_here",
            firstName: "Jean",
            lastName: "Dupont", // Représentant de l'entreprise
            userType: UserType.Company
        );

        await _userRepository.AddAsync(companyUser);

        List<User> storedUsers = await _userRepository.GetAllAsync();

        storedUsers
            .Should()
            .ContainSingle(u => u.Id == companyUser.Id && u.Email == companyUser.Email)
            .Which.UserType.Should().Be(UserType.Company);
    }

    [Fact]
    public async Task User_ShouldUpdateProfile()
    {
        // Scénario 3: Un utilisateur met à jour son profil
        User user = new User(
            userId: Guid.NewGuid(),
            email: "marie.martin@example.com",
            passwordHash: "$2a$11$hashed_password",
            firstName: "Marie",
            lastName: "Martin",
            userType: UserType.Student
        );

        // L'utilisateur met à jour son nom après mariage
        user.UpdateProfile("Marie", "Dubois");

        await _userRepository.AddAsync(user);

        List<User> storedUsers = await _userRepository.GetAllAsync();

        storedUsers
            .Should()
            .ContainSingle(u => u.Id == user.Id)
            .Which.LastName.Should().Be("Dubois");
    }
    
    #endregion

    #region Company data
    [Fact]
    public async Task AddAsync_ShouldInsertCompanyDocument()
    {
        Company company = new Company(
            companyId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            companyName: "Ales Company");

        // Mettre à jour le profil avec les informations supplémentaires
        company.UpdateProfile(
            companyName: "Ales Company 2",
            description: "lorem ipsum dolor sit amet consectetur adipiscing elit",
            industry: "AI - technologie",
            website: "https://alescompany.com",
            location: "Ales",
            employeeCount: 10);

        await _companyRepository.AddAsync(company);

        List<Company> storedCompanies = await _companyRepository.GetAllAsync();

        storedCompanies
            .Should()
            .ContainSingle(c => c.Id == company.Id && c.CompanyName == company.CompanyName)
            .Which.Description.Should().Be("lorem ipsum dolor sit amet consectetur adipiscing elit");
    }
    #endregion

    #region Student data
    [Fact]
    public async Task AddAsync_ShouldInsertStudentDocument()
    {
        Student student = new Student(
            userId: Guid.NewGuid(),
            studentId: Guid.NewGuid());

        student.UpdateProfile(
            phone: "123456789",
            bio: "lorem ipsum dolor sit amet consectetur adipiscing elit",
            location: "Ales",
            resumeUrl: "https://resume.com",
            skills: new List<string> { "C#", "MongoDB" },
            experienceYears: 2);

        await _studentRepository.AddAsync(student);

        List<Student> storedStudents = await _studentRepository.GetAllAsync();

        storedStudents
            .Should()
            .ContainSingle(s => s.Id == student.Id && s.UserId == student.UserId)
            .Which.StudentId.Should().Be(student.StudentId);
    }
    #endregion

    #region Application data
    
    [Fact]
    public async Task AddAsync_ShouldCreatePendingApplication()
    {
        // Scénario 1: Un étudiant postule à une offre (statut initial: Pending)
        Application application = new Application(
            studentId: Guid.NewGuid(),
            jobId: Guid.NewGuid(),
            coverLetter: "Madame, Monsieur,\n\nJe suis actuellement en dernière année de Master en Informatique et je suis très intéressé par le poste de Développeur .NET en alternance au sein de votre entreprise.\n\nMon parcours académique et mes projets personnels m'ont permis de développer des compétences solides en C#, ASP.NET Core et MongoDB.\n\nCordialement,\nJohn Doe",
            resumeUrl: "https://storage.example.com/resumes/john-doe-cv-2025.pdf");

        await _applicationRepository.AddAsync(application);

        List<Application> storedApplications = await _applicationRepository.GetAllAsync();

        storedApplications
            .Should()
            .ContainSingle(a => a.Id == application.Id)
            .Which.Should().Match<Application>(a => 
                a.Status == ApplicationStatus.Pending &&
                a.ReviewedAt == null &&
                a.AppliedAt <= DateTime.UtcNow);
    }

    [Fact]
    public async Task Application_ShouldBeAcceptedAfterReview()
    {
        // Scénario 2: L'entreprise examine la candidature et l'accepte
        Application application = new Application(
            studentId: Guid.NewGuid(),
            jobId: Guid.NewGuid(),
            coverLetter: "Je souhaite rejoindre votre équipe pour développer mes compétences en développement web.",
            resumeUrl: "https://storage.example.com/resumes/marie-martin-cv.pdf");

        // L'entreprise examine la candidature
        application.Review();
        
        // Après examen, l'entreprise accepte la candidature
        application.Accept();

        await _applicationRepository.AddAsync(application);

        List<Application> storedApplications = await _applicationRepository.GetAllAsync();

        Application? stored = storedApplications.FirstOrDefault(a => a.Id == application.Id);
        
        stored.Should().NotBeNull();
        stored!.Status.Should().Be(ApplicationStatus.Accepted);
        stored.ReviewedAt.Should().NotBeNull();
        stored.ReviewedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task Application_ShouldBeRejectedAfterReview()
    {
        // Scénario 3: L'entreprise examine la candidature mais la rejette
        Application application = new Application(
            studentId: Guid.NewGuid(),
            jobId: Guid.NewGuid(),
            coverLetter: "Je suis intéressé par cette opportunité.",
            resumeUrl: "https://storage.example.com/resumes/pierre-dupont-cv.pdf");

        // L'entreprise examine la candidature
        application.Review();
        
        // Après examen, l'entreprise rejette la candidature (profil ne correspond pas)
        application.Reject();

        await _applicationRepository.AddAsync(application);

        List<Application> storedApplications = await _applicationRepository.GetAllAsync();

        Application? stored = storedApplications.FirstOrDefault(a => a.Id == application.Id);
        
        stored.Should().NotBeNull();
        stored!.Status.Should().Be(ApplicationStatus.Rejected);
        stored.ReviewedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task Application_ShouldStayInReviewedStatus()
    {
        // Scénario 4: L'entreprise a examiné la candidature mais n'a pas encore pris de décision
        Application application = new Application(
            studentId: Guid.NewGuid(),
            jobId: Guid.NewGuid(),
            coverLetter: "Passionné par le développement logiciel, je souhaite mettre mes compétences au service de votre entreprise.",
            resumeUrl: "https://storage.example.com/resumes/sophie-bernard-cv.pdf");

        // L'entreprise examine la candidature mais attend avant de décider
        application.Review();

        await _applicationRepository.AddAsync(application);

        List<Application> storedApplications = await _applicationRepository.GetAllAsync();

        Application? stored = storedApplications.FirstOrDefault(a => a.Id == application.Id);
        
        stored.Should().NotBeNull();
        stored!.Status.Should().Be(ApplicationStatus.Reviewed);
        stored.ReviewedAt.Should().NotBeNull();
    }
    
    #endregion

    #region Integration Tests - Complete Workflow
    
    [Fact]
    public async Task CompleteWorkflow_StudentRegistrationToApplication()
    {
        // ÉTAPE 1: Un étudiant s'inscrit (création du User)
        User studentUser = new User(
            userId: Guid.NewGuid(),
            email: "thomas.bernard@student.com",
            passwordHash: "$2a$11$hashed_password",
            firstName: "Thomas",
            lastName: "Bernard",
            userType: UserType.Student
        );
        await _userRepository.AddAsync(studentUser);

        // ÉTAPE 2: L'étudiant complète son profil (création du Student)
        Student student = new Student(
            userId: studentUser.UserId,
            studentId: Guid.NewGuid()
        );
        
        // Ajouter des compétences
        student.AddSkill("C#");
        student.AddSkill("ASP.NET Core");
        student.AddSkill("React");
        
        // Mettre à jour le profil
        student.UpdateProfile(
            phone: "+33 6 12 34 56 78",
            bio: "Étudiant en Master 2 Informatique, passionné par le développement web et mobile.",
            location: "Paris, France",
            resumeUrl: "https://storage.example.com/resumes/thomas-bernard-cv.pdf",
            skills: student.Skills,
            experienceYears: 2
        );
        await _studentRepository.AddAsync(student);

        // ÉTAPE 3: Une entreprise publie une offre (simulé avec un Job existant)
        Job job = new Job(
            jobId: Guid.NewGuid(),
            companyId: Guid.NewGuid(),
            title: "Développeur Full Stack - Alternance",
            description: "Rejoignez notre équipe pour développer des applications web modernes.",
            location: "Paris",
            contractType: ContractType.Alternance,
            salary: 35000m
        );
        job.AddRequiredSkill("C#");
        job.AddRequiredSkill("React");
        await _jobRepository.AddAsync(job);

        // ÉTAPE 4: L'étudiant postule à l'offre
        Application application = new Application(
            studentId: student.StudentId,
            jobId: job.JobId,
            coverLetter: "Madame, Monsieur,\n\nJe suis très intéressé par le poste de Développeur Full Stack en alternance. Mon expérience en C# et React correspond parfaitement à vos besoins.\n\nCordialement,\nThomas Bernard",
            resumeUrl: "https://storage.example.com/resumes/thomas-bernard-cv.pdf"
        );
        await _applicationRepository.AddAsync(application);

        // VÉRIFICATIONS
        List<User> users = await _userRepository.GetAllAsync();
        List<Student> students = await _studentRepository.GetAllAsync();
        List<Job> jobs = await _jobRepository.GetAllAsync();
        List<Application> applications = await _applicationRepository.GetAllAsync();

        // Vérifier que le User existe
        users.Should().ContainSingle(u => u.UserId == studentUser.UserId)
            .Which.Email.Should().Be("thomas.bernard@student.com");

        // Vérifier que le Student est lié au User
        students.Should().ContainSingle(s => s.UserId == studentUser.UserId)
            .Which.Skills.Should().Contain(new[] { "C#", "ASP.NET Core", "React" });

        // Vérifier que le Job existe
        jobs.Should().ContainSingle(j => j.JobId == job.JobId)
            .Which.Title.Should().Be("Développeur Full Stack - Alternance");

        // Vérifier que l'Application est liée au Student et au Job
        applications.Should().ContainSingle(a => a.StudentId == student.StudentId && a.JobId == job.JobId)
            .Which.Status.Should().Be(ApplicationStatus.Pending);
    }

    [Fact]
    public async Task CompleteWorkflow_CompanyRegistrationToJobPosting()
    {
        // ÉTAPE 1: Une entreprise s'inscrit (création du User)
        User companyUser = new User(
            userId: Guid.NewGuid(),
            email: "hr@innovtech.fr",
            passwordHash: "$2a$11$hashed_password",
            firstName: "Sophie",
            lastName: "Durand", // RH de l'entreprise
            userType: UserType.Company
        );
        await _userRepository.AddAsync(companyUser);

        // ÉTAPE 2: L'entreprise complète son profil (création du Company)
        Company company = new Company(
            companyId: Guid.NewGuid(),
            userId: companyUser.UserId,
            companyName: "InnovTech Solutions"
        );
        company.UpdateProfile(
            companyName: "InnovTech Solutions",
            description: "Leader français dans le développement de solutions logicielles innovantes.",
            industry: "Technologies de l'information",
            website: "https://innovtech.fr",
            location: "Lyon, France",
            employeeCount: 150
        );
        await _companyRepository.AddAsync(company);

        // ÉTAPE 3: L'entreprise publie une offre d'alternance
        Job job = new Job(
            jobId: Guid.NewGuid(),
            companyId: company.CompanyId,
            title: "Développeur Backend .NET - Alternance",
            description: "Nous recherchons un développeur backend passionné pour rejoindre notre équipe.",
            location: "Lyon",
            contractType: ContractType.Alternance,
            salary: 32000m
        );
        job.AddRequiredSkill("C#");
        job.AddRequiredSkill("ASP.NET Core");
        job.AddRequiredSkill("MongoDB");
        job.Publish();
        await _jobRepository.AddAsync(job);

        // VÉRIFICATIONS
        List<User> users = await _userRepository.GetAllAsync();
        List<Company> companies = await _companyRepository.GetAllAsync();
        List<Job> jobs = await _jobRepository.GetAllAsync();

        // Vérifier que le User entreprise existe
        users.Should().ContainSingle(u => u.UserId == companyUser.UserId)
            .Which.UserType.Should().Be(UserType.Company);

        // Vérifier que la Company est liée au User
        companies.Should().ContainSingle(c => c.UserId == companyUser.UserId)
            .Which.CompanyName.Should().Be("InnovTech Solutions");

        // Vérifier que le Job est lié à la Company et est actif
        jobs.Should().ContainSingle(j => j.CompanyId == company.CompanyId)
            .Which.Should().Match<Job>(j =>
                j.Status == Status.Active &&
                j.RequiredSkills.Contains("C#") &&
                j.RequiredSkills.Contains("MongoDB"));
    }
    
    #endregion

    // Optional: Add a cleanup method if you want to reset the database after tests
    // [OneTimeTearDown] or implement IDisposable for cleanup

}