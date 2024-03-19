using Localization.Resources.AbpUi;
using Linkd.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Microsoft.Extensions.DependencyInjection;
using Linkd.Interfaces;
using Linkd.Services;
using Linkd.Repository;
using Linkd.IRepository;
using Volo.Abp.Users;

namespace Linkd;

[DependsOn(
    typeof(LinkdApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class LinkdHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
        context.Services.AddScoped<IPostService, PostService>();
        context.Services.AddScoped<IPostRepository, PostRepository>();
        context.Services.AddScoped<IConnectionRequestService, ConnectionRequestService>();
        context.Services.AddScoped<IConnectionRequestRepository, ConnectionRequestRepository>();
        context.Services.AddScoped<ILikeRepository,LikePostRepository>();
        context.Services.AddScoped<ILikeService, LikeService>();
        context.Services.AddScoped<ICommentRepository, CommentRepository>();
        context.Services.AddScoped<ICommentService, CommentService>();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<LinkdResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
