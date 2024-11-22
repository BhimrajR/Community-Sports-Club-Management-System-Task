using System.Globalization;

public class Person
{
    public Person(string name, int age, string contactNumber) 
    {
        this.name = name;
        this.age = age;
        this.contactNumber = contactNumber;
    }

    public string name;
    public int age;
    public string contactNumber;

    public void SetDetails(string name, int age, string contactNumber)
    {
        this.name = name;
        this.age = age;
        this.contactNumber = contactNumber;
    }

    public string GetDetails()
    {
        return $"Name: {name}\nAge: {age} \nContact Number: {contactNumber}\n";
    }
}

public class Member : Person
{
    public Member(string name, int age, string contactNumber, string membershipID, string sport) : base(name, age, contactNumber)
    {
        this.membershipID = membershipID;
        this.sport = sport;
    }

    public string membershipID;
    public string sport;

    public List<int> performanceScores = new List<int>();

    public void SetMembershipDetails(string membershipID, string sport)
    {
        this.membershipID = membershipID;
        this.sport = sport;
    }

    public void AddPerformanceScores(int score)
    {
        performanceScores.Add(score);
    }

    public float CalculateAverageScore()
    {
        return (performanceScores.Count == 0) ? 0 : ((float)performanceScores.Sum())/performanceScores.Count();
    }

    public string GetMemberSummary()
    {
        return GetDetails() + $"MembershipID: {membershipID}\nSport: {sport}\n";
    }
}

public class Coach : Person
{
    public Coach(string name, int age, string contactNumber, string coachID, string specialisation, int salary) : base(name, age, contactNumber)
    {
        this.coachID = coachID;
        this.specialisation = specialisation;
        this.salary = salary;
    }

    public string coachID;
    public string specialisation;
    public int salary;
    public List<Member> mentees = new List<Member>();

    public List<Coach> mentorshipGroup = new List<Coach>();

    public void SetCoachDetails(string coachID, string specialisation, int salary)
    {
        this.coachID = coachID;
        this.specialisation = specialisation;
        this.salary = salary;
    }

    public void AssignMentee(Member mentee)
    {
        if (!mentees.Contains(mentee)) mentees.Add(mentee);
        Console.WriteLine($"Coach {name} is now mentoring {mentee.name} in {mentee.sport}.");
    }

    public List<string> GetMentees()
    {
        return mentees.Select(x => $"{x.name}: {x.sport}").ToList();
    }

    public void MentorCoach(Coach menteeCoach)
    {
        if (!mentorshipGroup.Contains(menteeCoach)) mentorshipGroup.Add(menteeCoach);
        Console.WriteLine($"Coach {name} is now mentoring Coach {menteeCoach.name} in {menteeCoach.specialisation}");
    }

    public List<string> GetMentorshipGroup()
    {
        return mentorshipGroup.Select(x => $"{x.name}: {x.specialisation}").ToList();
    }

    public void IncreaseSalary(float percentage)
    {
        salary += (int)(salary * percentage / 100);
    }

    public string GetCoachSummary()
    {
        return GetDetails() + $"CoachID: {coachID}\nSpecialisation: {specialisation}\nSalary: {salary}\n";
    }
}

public class Staff : Person
{
    public Staff(string name, int age, string contactNumber, string staffID, string position, int yearsOfService) : base(name, age, contactNumber)
    {
        this.staffID = staffID;
        this.position = position;
        this.yearsOfService = yearsOfService;
    }

    public string staffID;
    public string position;
    public int yearsOfService;

    public void SetStaffDetails(string staffID, string position, int yearsOfService)
    {
        this.staffID = staffID;
        this.position = position;
        this.yearsOfService = yearsOfService;
    }

    public void IncrementYearsOfService()
    {
        yearsOfService++;
    }

    public void AssistMember(Member member)
    {
        Console.WriteLine($"Staff {name} assisted {member.name} in resolving an issue.");
    }

    public string GetStaffSummary()
    {
        return GetDetails() + $"StaffID: {staffID}\nPosition: {position}\nYears Of Service: {yearsOfService}\n";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Member member1 = new Member("Adam", 19, "0123456789", "28DJB", "Football");
        Member member2 = new Member("Jessica", 21, "03156482", "06BHJ", "Basketball");

        Coach coach1 = new Coach("Josh", 30, "283946589", "92DOA", "Tennis", 80000);
        Coach coach2 = new Coach("Mike", 37, "849521368", "64AOS", "Badminton", 60000);

        Staff staff1 = new Staff("Alex", 24, "273930098", "83AKS", "Club Manager", 3);

        member1.AddPerformanceScores(100);
        member1.AddPerformanceScores(20);
        member1.AddPerformanceScores(30);
        Console.WriteLine(member1.CalculateAverageScore());

        member2.CalculateAverageScore();

        Console.WriteLine(member1.GetMemberSummary());
        Console.WriteLine(member2.GetMemberSummary());
        coach1.AssignMentee(member1);
        coach1.AssignMentee(member2);
        coach2.AssignMentee(member2);
        coach2.MentorCoach(coach1);

        Console.WriteLine(string.Join(", ", coach1.GetMentees()));
        Console.WriteLine(string.Join(", ", coach2.GetMentees()));
        Console.WriteLine(string.Join(", ", coach2.GetMentorshipGroup()));

        coach2.IncreaseSalary(20);
        Console.WriteLine(coach1.GetCoachSummary());
        Console.WriteLine(coach2.GetCoachSummary());

        staff1.AssistMember(member1);
        staff1.IncrementYearsOfService();
        Console.WriteLine(staff1.GetStaffSummary());
    }
}