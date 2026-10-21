
using LendTrack.Core;

namespace LendTrack.Tests;

public class PersonAndLoanTests
{
    [Fact]
    public void PersonInitialiseTest()
    {
        // Arrange
        string name = "Test Person";

        // Act
        Person person = new(name);

        // Assert
        Assert.Equal(name, person.Name);
    }

    [Fact]
    public void PersonBorrowedHeavyItemsTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);

        // Act
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Assert
        Assert.Single(person.TakenLoans);
        Assert.Equal(loan, person.TakenLoans[0]);
    }

    [Fact]
    public void PersonBorrowedLightItemsTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Light Item", "A light item", ItemType.Tool, 1);

        // Act
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Assert
        Assert.Single(person.TakenLoans);
        Assert.Equal(loan, person.TakenLoans[0]);
    }

    [Fact]
    public void LoanIsNotOverdueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act

        loan.UpdateOverdueStatus(DateTime.Now.AddDays(10)); // Simulate checking after 10 days

        // Assert
        Assert.False(loan.OverdueStatus);
    }

    [Fact]
    public void LoanIsOverdueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        loan.UpdateOverdueStatus(DateTime.Now.AddDays(15)); // Simulate checking after 15 days

        // Assert
        Assert.True(loan.OverdueStatus);
    }

    [Fact]
    public void CanAPersonBorrowItemOneParameterTrueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person);

        // Assert
        Assert.True(canBorrow);
    }

    [Fact]
    public void CanAPersonBorrowItemOneParameterFalseTooManyTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);

        Item testItemOne = new("Test Item One", "An item", ItemType.Electronics, 1);
        Item testItemTwo = new("Test Item Two", "Another test item", ItemType.Electronics, 1);
        Item testItemThree = new("Test Item Three", "Yet another test item", ItemType.Electronics, 1);
        Item testItemFour = new("Test Item Four", "A fourth test item", ItemType.Electronics, 1);
        Item testItemFive = new("Test Item Five", "A fifth test item", ItemType.Electronics, 1);

        Loan loanOne = new(person, testItemOne, new TimeSpan(14, 0, 0, 0));
        Loan loanTwo = new(person, testItemTwo, new TimeSpan(14, 0, 0, 0));
        Loan loanThree = new(person, testItemThree, new TimeSpan(14, 0, 0, 0));
        Loan loanFour = new(person, testItemFour, new TimeSpan(14, 0, 0, 0));
        Loan loanFive = new(person, testItemFive, new TimeSpan(14, 0, 0, 0));

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person);

        // Assert
        Assert.False(canBorrow); // The person has already borrowed 5 items, which is the maximum allowed
    }

    [Fact]
    public void CanAPersonBorrowItemOneParameterFalseOverdueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));
        DateTime overdueDate = DateTime.Now.AddDays(15); // Simulate checking after 15 days

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person, overdueDate);

        // Assert
        Assert.False(canBorrow); // The person has an overdue item and cannot borrow more
    }

    [Fact]
    public void CanAPersonBorrowItemTwoParemetersTrueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItemOne = new("Test Item One", "An item", ItemType.Electronics, 1);
        Item testItemTwo = new("Test Item Two", "Another test item", ItemType.Electronics, 1);
        Loan loan = new(person, testItemOne, new TimeSpan(14, 0, 0, 0));

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person, testItemTwo);

        // Assert
        Assert.True(canBorrow); // The person has not borrowed the item yet
    }

    [Fact]
    public void CanAPersonBorrowItemTwoParemetersFalseTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person, testItem);

        // Assert
        Assert.False(canBorrow); // The person has already borrowed the item
    }

    [Fact]
    public void CanAPersonBorrowItemTwoParemetersFalseOverdueTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Simulate the loan being overdue by checking after 15 days
        loan.UpdateOverdueStatus(DateTime.Now.AddDays(15));
        Assert.True(loan.OverdueStatus); // Ensure the loan is indeed overdue

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person, testItem);

        // Assert
        Assert.False(canBorrow); // The person has an overdue item and cannot borrow more
    }

    [Fact]
    public void CanAPersonBorrowItemTwoParemetersFalseTooManyTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);

        Item testItemOne = new("Test Item One", "An item", ItemType.Electronics, 1);
        Item testItemTwo = new("Test Item Two", "Another test item", ItemType.Electronics, 1);
        Item testItemThree = new("Test Item Three", "Yet another test item", ItemType.Electronics, 1);
        Item testItemFour = new("Test Item Four", "A fourth test item", ItemType.Electronics, 1);
        Item testItemFive = new("Test Item Five", "A fifth test item", ItemType.Electronics, 1);

        Loan loanOne = new(person, testItemOne, new TimeSpan(14, 0, 0, 0));
        Loan loanTwo = new(person, testItemTwo, new TimeSpan(14, 0, 0, 0));
        Loan loanThree = new(person, testItemThree, new TimeSpan(14, 0, 0, 0));
        Loan loanFour = new(person, testItemFour, new TimeSpan(14, 0, 0, 0));
        Loan loanFive = new(person, testItemFive, new TimeSpan(14, 0, 0, 0));

        // Act
        bool canBorrow = Loan.CanAPersonBorrowItem(person, testItemOne);

        // Assert
        Assert.False(canBorrow); // The person has already borrowed the maximum number of items
    }

    [Fact]
    public void PersonToStringTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        string personString = person.ToString();

        // Assert
        Assert.Contains(name, personString);
        Assert.Contains(person.Id.ToString(), personString);
        Assert.Contains(person.TakenLoans.Count.ToString(), personString);
    }

    [Fact]
    public void LoanToStringTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        string loanString = loan.ToString();

        // Assert
        Assert.Contains(person.Name, loanString);
        Assert.Contains(testItem.Name!, loanString);
        Assert.Contains(loan.LoanDate.ToShortDateString(), loanString);
        Assert.Contains(loan.LoanDuration.Days.ToString(), loanString);
    }

    [Fact]
    public void LoanExceptionWhenCannotBorrowTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loanOne = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        Action act = () => new Loan(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void PersonEqualsTest()
    {
        // Arrange
        string name = "Test Person";
        Person personOne = new(name);
        Person personTwo = new(name);

        // Act
        bool areEqual = personOne.Equals(personTwo);

        // Assert
        Assert.False(areEqual); // They should not be equal because they have different Ids
    }

    [Fact]
    public void UpdateOverdueStatusTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        loan.UpdateOverdueStatus(DateTime.Now.AddDays(15)); // Simulate checking after 15 days

        // Assert
        Assert.True(loan.OverdueStatus); // The loan should be marked as overdue
    }

    [Fact]
    public void UpdateOverdueStatusParemeterlessFalseTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        loan.UpdateOverdueStatus(); // Simulate checking after 15 days

        // Assert
        Assert.False(loan.OverdueStatus); // The loan should be marked as overdue
    }

    [Fact]
    public void PersonGetHashCodeTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);

        // Act
        int hashCode = person.GetHashCode();

        // Assert
        Assert.Equal(person.Id.GetHashCode(), hashCode);
    }

    [Fact]
    public void LoanGetHashCodeTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));

        // Act
        int hashCode = loan.GetHashCode();

        //Assert
        Assert.Equal(loan.Id.GetHashCode(), hashCode);
    }

    [Fact]
    public void ReturnItemTest()
    {
        // Arrange
        string name = "Test Person";
        Person person = new(name);
        Item testItem = new("Test Item", "An item", ItemType.Electronics, 1);
        Loan loan = new(person, testItem, new TimeSpan(14, 0, 0, 0));
        // Act
        loan.ReturnItem();
        // Assert
        Assert.True(testItem.IsAvailable); // The item should be available after returning
        Assert.Empty(person.TakenLoans); // The person's taken loans should be empty after returning the item
    }
}
