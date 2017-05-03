using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDotNext.AsyncVoid
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"M {Thread.CurrentThread.ManagedThreadId}");
            MyMethod().Wait();
            Console.WriteLine($"M {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        private static async Task MyMethod()
        {
            Console.WriteLine($"0 {Thread.CurrentThread.ManagedThreadId}");
            Task t1 = Task.Delay(1000);
            Console.WriteLine($"1 {Thread.CurrentThread.ManagedThreadId}");
            await t1;
            Console.WriteLine($"2 {Thread.CurrentThread.ManagedThreadId}");
            Task t2 = Task.Delay(1000);
            Console.WriteLine($"3 {Thread.CurrentThread.ManagedThreadId}");
            await t2;
            Console.WriteLine($"4 {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private async static Task MainAsync()
        {
            /*
             * Никакого исключения мы не увидели, приложение завершилось раньше.
             * Если мы поставим какую-то задержку, мы сможем застать исключение.
             * 
             * DelayAndTrowAsync не возвращает Task, на нечего await-ить - мы
             * не можем поймать исключения асинхронного метода.
             * 
             * async void
             * Исключения выбразываются в вызывающий контекст (могут завершить процесс)
             * Не узнаем об окончании операции (Fire and forget)
             * Только для обработчиков событий и подобных штук
             * 
             * Для консольного приложения контекстом является пул потоков - многопоточный контекст
            */
            Console.WriteLine("Started");
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                DelayAndTrowAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occured: {e.Message}");
            }

            Console.WriteLine("Finished");

            // Если мы поставим какую-то задержку, мы сможем застать исключение
            //Console.ReadLine();
        }

        private static async void DelayAndTrowAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException();
        }
    }
}
