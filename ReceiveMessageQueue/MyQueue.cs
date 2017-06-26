using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace ReceiveMessageQueue
{
    public class MyQueue
    {
        static void Main(string[] args)
        {
            MyQueue queue = new MyQueue();
            while (true)
            {
                MyQueue.receiveMessage(); 
            }
            Console.Read();
        }

        static void receiveMessage()
        {
            // Connect to a transactional queue on the local computer.
            MessageQueue myQueue = new
                MessageQueue(".\\mymsgqueue");

            // Set the formatter.
            myQueue.Formatter = new XmlMessageFormatter(new Type[]
                {typeof(String)});

            // Create a transaction.
            MessageQueueTransaction myTransaction = new
                MessageQueueTransaction();

            try
            {
                // Begin the transaction.
                myTransaction.Begin();

                // Receive the message. 
                Message myMessage = myQueue.Receive(myTransaction);
                String myOrder = (String)myMessage.Body;

                // Display message information.
                Console.WriteLine(myOrder);

                // Commit the transaction.
                myTransaction.Commit();

            }

            catch (MessageQueueException e)
            {
                // Handle nontransactional queues.
                if (e.MessageQueueErrorCode ==
                    MessageQueueErrorCode.TransactionUsage)
                {
                    Console.WriteLine("Queue is not transactional.");
                }

                // Else catch other sources of a MessageQueueException.

                // Roll back the transaction.
                myTransaction.Abort();
            }

            // Catch other exceptions as necessary, such as 
            // InvalidOperationException, thrown when the formatter 
            // cannot deserialize the message.

            return;
        }
    }
}
