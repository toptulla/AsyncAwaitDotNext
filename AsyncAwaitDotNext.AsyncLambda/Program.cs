using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDotNext.AsyncLambda
{
    class Program
    {
        static void Main()
        {
            TrueWrapAsyncVoid().Wait();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private async static Task MainAsync()
        {
            /*
             * Та же ошибка - async void
             * Каждый раз когда вы собираетесь написать асинхронную лямбду или
             * видите ее в коде - проверьте тип делегата! Если это Action, если
             * тип делегата возвращает void, значит что-то тут может пойти не так!
            */
            Console.WriteLine("Started");
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Foo(async () => { throw new NotImplementedException(); });

                // Аналогично без оберточного метода Foo
                //Action a = async () => { throw new NotImplementedException(); };
                //a();

                //Task t = null;
                //SomeMethod(() => t = FooAsync());
                // await t;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occured: {e.Message}");
            }

            Console.WriteLine("Finished");

            await Task.Delay(1000);
        }

        private static void Foo(Action p)
        {
            p();
        }

        private async static Task WithTrueAsyncLambda()
        {
            Console.WriteLine("Started");
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Func<Task> lambda = async () => {
                    Console.WriteLine($"lambda 1 {Thread.CurrentThread.ManagedThreadId}");
                    await Task.Delay(3000);
                    Console.WriteLine($"lambda 2 {Thread.CurrentThread.ManagedThreadId}");
                    throw new NotImplementedException();
                };
                await lambda();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occured: {e.Message}");
            }

            Console.WriteLine("Finished");

            Console.WriteLine($"WithTrueAsyncLambda 1 {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(3000);
            Console.WriteLine($"WithTrueAsyncLambda 2 {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task TrueWrapAsyncVoid()
        {
            Console.WriteLine("Started");
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                Task t = null;
                SomeMwthod(() => t = DelayAndTrowAsync());
                await t;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occured: {e.Message}");
            }

            Console.WriteLine("Finished");
        }

        private static void SomeMwthod(Action action)
        {
            action();
        }

        private static async Task DelayAndTrowAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException();
        }
    }
}
