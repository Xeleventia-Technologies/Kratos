namespace Kratos.Api.Database.Models;

public static class Enums
{
    public enum OtpPurpose { SignUp, ResetPassword }
    public enum LoggedInWith { Email, Google }
    public enum BlogApprovalStaus { Pending, Approved, Rejected }
    public enum BlogVoteType { Upvote, Downvote }
    public enum ForumThreadVoteType { Upvote, Downvote }
}
