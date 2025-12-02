namespace ordination_test;
using shared.Model;

[TestClass]
public class OrdinationTest
{
    private Laegemiddel CreateLm()
    {
        return new Laegemiddel(
            "TestMedicin",
            0.1,  
            0.2,  
            0.3,  
            "Stk"
        );
    }

    [TestMethod]
    public void AntalDage_SameDay_ReturnsOne()
    {
        var ord = new PN(
            new DateTime(2025, 1, 10),
            new DateTime(2025, 1, 10),
            1,
            CreateLm()
        );

        int result = ord.antalDage();

        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void AntalDage_ThreeDays_ReturnsCorrectValue()
    {
        var ord = new PN(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 3),
            1,
            CreateLm()
        );

        int result = ord.antalDage();

        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void AntalDage_LongPeriod_ReturnsCorrectValue()
    {
        var ord = new PN(
            new DateTime(2025, 1, 1),
            new DateTime(2025, 1, 31),
            1,
            CreateLm()
        );

        int result = ord.antalDage();

        Assert.AreEqual(31, result);
    }

    // KORREKT VERSION:
    [TestMethod]
    public void AntalDage_StartAfterEnd_ReturnsZero()
    {
        var ord = new PN(
            new DateTime(2025, 1, 10),
            new DateTime(2025, 1, 5),
            1,
            CreateLm()
        );

        int result = ord.antalDage();

        Assert.AreEqual(0, result);
    }
}