using RFTools.Calculators;
using RFTools.Constants;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Tests.Unit;

public class CalculatorTests
{
    private readonly SkinDepth _sutSkinDepth = new();
    private readonly StandingWave _sutStandingWaves = new();
    private readonly Resonance _sutResonance = new();
    private readonly PathLoss _sutPathLoss = new();
    private readonly LinkBudget _sutLinkBudget = new();

    [Theory]
    [InlineData(0.000001678, 0.999991, 2_400_000_000, 1.3308)]
    [InlineData(0.000001678, 0.999991, 4_000_000_000, 1.0308)]
    [InlineData(0.000001678, 0.999991, 5_000_000_000, 0.922)]
    public void CalculateSkinDepth_ReturnResult_SameValue(
        double resistivity,
        double permeability,
        double frequency,
        double expected)
    {
        // Act
        var result = _sutSkinDepth.Calculate(resistivity, permeability, frequency);

        // Assert
        Assert.Equal(expected, result.Value, precision: 4);
    }

    [Fact]
    public void CalculateSkinDepth_ReturnResult_SameUnits()
    {
        // Act
        var result = _sutSkinDepth.Calculate(0.000001678, 0.999991, 2_400_000_000);

        // Assert
        Assert.Equal("µm", result.Unit);
    }

    [Theory]
    [InlineData(Known.Vswr, 5, 5, 0.67, 3.52, 2.55)]
    [InlineData(Known.ReflectionCoefficient, 0.5, 3, 0.5, 6.02, 1.25)]
    [InlineData(Known.ReturnLoss, 2, 8.72, 0.79, 2, 4.33)]
    public void CalculateStandingWave_ValidInputs_EqualResult(
        Known type,
        double input,
        double expectedVswr,
        double expectedReflectionCoefficient,
        double expectedReturnLoss,
        double expectedMismatchLoss)
    {
        // Act
        var result = _sutStandingWaves.Calculate(type, input);
        var actualVswr = result.FirstOrDefault(o => o.Name.Equals("VSWR"))!.Value;
        var actualReflectionCoefficient = result.FirstOrDefault(o => o.Name.Contains("Reflection Coefficient"))!.Value;
        var actualReturnLoss = result.FirstOrDefault(o => o.Name.Equals("Return Loss"))!.Value;
        var actualMismatchLoss = result.FirstOrDefault(o => o.Name.Equals("Mismatch Loss"))!.Value;

        // Assert
        Assert.Multiple(
            () => Assert.Equal(expectedVswr, actualVswr, precision: 2),
            () => Assert.Equal(expectedReflectionCoefficient, actualReflectionCoefficient, precision: 2),
            () => Assert.Equal(expectedReturnLoss, actualReturnLoss, precision: 2),
            () => Assert.Equal(expectedMismatchLoss, actualMismatchLoss, precision: 2)
        );
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-0.5)]
    public void CalculateStandingWave_InvalidReflectionCoefficient_ThrowArgumentException(double input)
    {
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sutStandingWaves.Calculate(Known.ReflectionCoefficient, input));
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.5)]
    public void CalculateStandingWave_InvalidVSWR_ThrowArgumentException(double input)
    {
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sutStandingWaves.Calculate(Known.Vswr, input));
    }

    [Theory]
    [InlineData(Unknown.Frequency, 0, 4E-12, 5E-6, 35.588)]
    [InlineData(Unknown.Capacitance, 2.4E6, 0, 5E-6, 0.8795)]
    [InlineData(Unknown.Inductance, 2.4E6, 3E-10, 0, 14.66)]
    public void CalculateResonance_FindUnknownForValues_EqualResult(
        Unknown type, 
        double frequency,
        double capacitance, 
        double inductance,
        double expected)
    {
        // Arrange
        // Act
        Result result = default!;
        if (type == Unknown.Frequency)
            result = _sutResonance.Calculate(type, capacitance: capacitance, inductance: inductance);
        else if (type == Unknown.Capacitance)
            result = _sutResonance.Calculate(type, frequency: frequency, inductance: inductance);
        else if (type == Unknown.Inductance)
            result = _sutResonance.Calculate(type, frequency: frequency, capacitance: capacitance);

        // Assert
        Assert.Equal(expected, result.Value, precision: 2);
    }

    [Theory]
    [InlineData(6, 2.4E3, 0, 0, 115.61)]
    [InlineData(13, 5E3, 1.3, 6, 121.40)]
    [InlineData(4.54, 6E3, 0, 0.5, 120.64)]
    public void CalculatePathLoss_MultipleValues_EqualResult(
        double distance,
        double frequency,
        double txGain,
        double rxGain,
        double expected)
    {
        // Arrange
        // Act
        var result = _sutPathLoss.Calculate(txGain, rxGain, distance, frequency);

        // Assert
        Assert.Equal(expected, result.Value, precision: 2);
    }

    [Fact]
    public void CalculateLinkBudget_ValidValues_EqualResult()
    {
        // Arrange
        // Act
        var result = _sutLinkBudget.Calculate(12, 16, 0, 16, 0, 7, 2.4E2);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(-72.95, result[1].Value, precision: 2),
            () => Assert.Equal(115, result[2].Value, precision: 2)
        );
    }
}