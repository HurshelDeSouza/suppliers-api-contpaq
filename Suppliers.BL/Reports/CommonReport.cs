using Microsoft.EntityFrameworkCore;

namespace Suppliers.BL.Reports
{
    public class CommonReport<T, TContext, TFilter>(TFilter filter, TContext context) where TContext : DbContext
    {
        protected T Data { get; set; }
        protected TContext Context { get; set; } = context;
        protected TFilter Filter { get; set; } = filter;

        public virtual Task Init() => throw new NotImplementedException("Init not Implemented");
        protected virtual Task InitConfiguration() => throw new NotImplementedException("InitConfiguration not Implemented");
        protected virtual Task SetData() => throw new NotImplementedException("SetData not Implemented");

        public virtual string GeneratePdf(string fileName) => throw new NotImplementedException("GeneratePdf not implemented");
        public virtual string GenerateExcel(string fileName) => throw new NotImplementedException("GenerateExcel not implemented");
    }

    public class CommonReportConfig<T, TFilter> : CommonReport<T, DescargaCfdiGfpContext, TFilter>
    {
        private string KeyLogo { get; set; } = "";
        private string KeyPath { get; set; } = "";
        protected string Logo { get; set; } = "";
        protected string FilePath { get; set; } = "";

        public CommonReportConfig(TFilter filter, DescargaCfdiGfpContext context) : base(filter, context)
        {
            KeyLogo = "Logo";
            KeyPath = "rutareportecsv";
        }

        public CommonReportConfig(TFilter filter, DescargaCfdiGfpContext context, string keyLogo, string keyPath, string customLogo = "", string customPath = "") : base(filter, context)
        {
            KeyLogo = keyLogo;
            KeyPath = keyPath;
            Logo = customLogo;
            FilePath = customPath;
        }

        public override async Task Init()
        {
            await InitConfiguration();
            await SetData();
        }

        protected override async Task InitConfiguration()
        {

        }
    }
}
