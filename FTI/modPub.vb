Module modPub
    'webSQL
    Friend client As iSQL.webSQLClient

    'date format
    Friend ciTH As System.Globalization.CultureInfo
    Friend ciEN As System.Globalization.CultureInfo

    'logon details
    'Friend user_groups As Collection
    Friend user_name As String
    Friend user_pass As String
    Friend user_session As String
    Friend user_firstname As String
    Friend user_lastname As String
    Friend user_department As String
    Friend user_div As String

    'count deleted release
    'Friend FTImemberDeletedCNT As Integer

    'form
    Friend fFTI As frmFTI
    Friend fFee As frmFTIFees
    Friend fPermissions As frmMainPermissions
    Friend fCommitteeGroup As frmFTICommitteeGroups
    'Friend fCommitteeGroupDetails As frmFTICommitteeGroupsDetails
    Friend fCommittee As frmFTICommittee

    Friend fGS1 As frmGS1
    Friend fFeeGS1 As frmGS1Fees

    Friend fINI() As frmINI
    Friend fFeeINI() As frmINIFees

    Friend fFMain As frmFINMain
    Friend fPN As frmFINPaymentNotice
    Friend fIV As frmFINInvoice
    Friend fRC As frmFINReceipt

End Module
