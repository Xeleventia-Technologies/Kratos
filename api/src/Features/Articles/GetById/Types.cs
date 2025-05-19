namespace Kratos.Api.Features.Articles.GetById;

public record Article(
    long Id,
    string Title,
    string Summary,
    string Content,
    string? ImageFileName,
    string ApprovalStatus,
    string UserFullName,
    string UserDisplayPicture,
    string CreatedAt
);
