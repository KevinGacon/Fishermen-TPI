using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SellFishesSystemTest
{
    [Test]
    public void SellFishesSystemTestSellFishTooSmall()
    {

    }

    [Test]
    public void SellFishesSystemTestSellFishTooYoung()
    {

    }


    [Test]
    public void SellFishesSystemTestSellFishNotFresh()
    {

    }

    [Test]
    public void SellFishesSystemTestSellFishLargeAndOldEnough()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SellFishesSystemTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
