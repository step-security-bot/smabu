using LIT.Smabu.Shared;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LIT.Smabu.Infrastructure.Reports
{
    public class QuestReport(IDocument document) : IReport
    {
        public IDocument Document { get; } = document;

        public byte[] GeneratePdf()
        {
            return Document.GeneratePdf();
        }
    }
}
