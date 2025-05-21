namespace Kratos.Api.Database.Models;

public static class Enums
{
    public enum OtpPurpose { SignUp, ResetPassword }
    public enum LoggedInWith { Email, Google }
    public enum ArticleApprovalStatus { Pending, Approved, Rejected }
    public enum CommunityThreadVoteType { Upvote, Downvote }
    public enum AssignedOptedInServiceStatus { Pending, InProgress, Completed }
}
