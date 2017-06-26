using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace SendMessageQueue
{
    class MyQueue
    {
        static void Main(string[] args)
        {
            int m = 1;
            MyQueue queue = new MyQueue();
            for (int i = 0; i < 2000; i++)
            {
                MyQueue.sendMessage(m);
                m++; 
            }
            Console.Read();
        }

        static void sendMessage(int m)
        {
            // Connect to a queue on the local computer.
            MessageQueue myQueue = new MessageQueue(".\\mymsgqueue");

            // Send a message to the queue.
            if (myQueue.Transactional == true)
            {
                // Create a transaction.
                MessageQueueTransaction myTransaction = new
                    MessageQueueTransaction();

                // Begin the transaction.
                myTransaction.Begin();

                String message = "My Message Data. " + m;

                Console.WriteLine(  message);

                // Send the message.
                myQueue.Send(message, myTransaction);

                

                // Commit the transaction.
                myTransaction.Commit();
            }
            else
            {
                myQueue.Send("My Message Data.");
            }

            return;
        }
    }
}
