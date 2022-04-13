using CDRMonitorig.Domain.Rules.Interfaces;
using System.Text;

namespace CDRMonitorig.Console.Extensions
{
    public static class PrintExtensions
    {
        public static void ToConsoleOutput<T>(this IMultilineReport<T> report)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"report.Title:");
            sb.AppendLine();

            foreach (var item in report.Items)
            {
                sb.AppendLine(item?.ToString());
                sb.AppendLine();
            }

            System.Console.WriteLine(sb.ToString());
        }
    }
}
