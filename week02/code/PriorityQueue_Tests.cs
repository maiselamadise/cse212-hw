using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue A (Pri 1), B (Pri 5), C (Pri 3) in that order, regardless of priority.
    // Expected Result: Each item goes to the back of the queue, so the queue reads
    // "[A (Pri:1), B (Pri:5), C (Pri:3)]". (Requirement 1)
    // Defect(s) Found: None. PASSED against the original code. Enqueue already appended to the
    // back of the list without reordering.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("[A (Pri:1), B (Pri:5), C (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue A (Pri 1), B (Pri 5), C (Pri 3). Dequeue once.
    // Expected Result: "B" is returned (highest priority, in the middle), and the remaining queue
    // is "[A (Pri:1), C (Pri:3)]". (Requirement 2)
    // Defect(s) Found: FAILED against the original code. The correct value "B" was returned, but
    // Dequeue never removed the item from the list, so the queue still held all three items.
    // Fixed by calling RemoveAt on the chosen index.
    public void TestPriorityQueue_DequeueRemovesHighestPriorityFromMiddle()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("[A (Pri:1), C (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue A (Pri 1), B (Pri 3), C (Pri 5), where the highest priority item is
    // the LAST one in the queue. Dequeue once.
    // Expected Result: "C" is returned. (Requirement 2)
    // Defect(s) Found: FAILED against the original code. The search loop used
    // "index < _queue.Count - 1", so the last item in the queue was never examined and "B" was
    // returned instead of "C". Fixed by looping to "index < _queue.Count".
    public void TestPriorityQueue_HighestPriorityAtBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (Pri 9), B (Pri 2), C (Pri 3), where the highest priority item is the
    // FIRST one in the queue. Dequeue once.
    // Expected Result: "A" is returned and the remaining queue is "[B (Pri:2), C (Pri:3)]".
    // (Requirement 2)
    // Defect(s) Found: FAILED against the original code, only because of the missing removal
    // (A was returned correctly but was still in the queue afterwards). Fixed with RemoveAt.
    public void TestPriorityQueue_HighestPriorityAtFront()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 9);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("[B (Pri:2), C (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue A (Pri 5), B (Pri 5), C (Pri 1). Dequeue once.
    // Expected Result: "A" is returned, because on a priority tie the item closest to the front
    // wins. (Requirement 3)
    // Defect(s) Found: FAILED against the original code. The comparison used ">=", so a later item
    // with an equal priority replaced the earlier one and "B" was returned instead of "A".
    // Fixed by changing the comparison to ">".
    public void TestPriorityQueue_TieGoesToFrontItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (1), B (5), C (3), D (5), E (2), then dequeue everything.
    // Expected Result: B, D, C, E, A. This mixes the highest priority, ties, and repeated dequeues.
    // (Requirements 2 and 3)
    // Defect(s) Found: FAILED against the original code. The first Dequeue returned "D" instead of
    // "B" (the ">=" tie defect). Because nothing was ever removed, repeated Dequeue calls would
    // also have kept returning the same item. All three defects (">=", skipped last item, no
    // removal) are fixed.
    public void TestPriorityQueue_FullDrainOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 5);
        priorityQueue.Enqueue("E", 2);

        string[] expected = ["B", "D", "C", "E", "A"];
        foreach (var value in expected)
        {
            Assert.AreEqual(value, priorityQueue.Dequeue());
        }
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue a single item A (Pri 1), then Dequeue it.
    // Expected Result: "A" is returned and the queue is then empty ("[]").
    // Defect(s) Found: FAILED against the original code. "A" was returned, but it was never
    // removed, so the queue still contained it. Fixed with RemoveAt.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeue from a brand new, empty queue.
    // Expected Result: An InvalidOperationException with the message "The queue is empty."
    // (Requirement 4)
    // Defect(s) Found: None. PASSED against the original code. The empty check threw the
    // correct exception type and message.
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue one item, Dequeue it, then Dequeue again from the now-empty queue.
    // Expected Result: The second Dequeue throws an InvalidOperationException with the message
    // "The queue is empty." (Requirement 4)
    // Defect(s) Found: FAILED against the original code. Because Dequeue never removed items,
    // the queue was never empty and the second call returned "A" again instead of throwing.
    // Fixed by removing the item on Dequeue.
    public void TestPriorityQueue_EmptyAfterDrainingThrows()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}
