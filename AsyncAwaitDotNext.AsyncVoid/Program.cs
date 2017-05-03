using System;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDotNext.AsyncVoid
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();
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
