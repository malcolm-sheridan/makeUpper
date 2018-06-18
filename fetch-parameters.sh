#!/bin/sh

AccountDNS=`aws --region ap-southeast-2 cloudformation list-exports --query "Exports[?Name=='account-hosted-zone-name'].Value" --output text`
AcmCertificateArn=`aws --region us-east-1 acm list-certificates --query "CertificateSummaryList[?DomainName=='*.${AccountDNS}'].CertificateArn" --output text`

cat <<EOF > parameters.json
{
    "AcmCertificateArn": "${AcmCertificateArn}",
    "CommitHash": "${CODEBUILD_RESOLVED_SOURCE_VERSION}",
    "PathToArtifact": "${CODEBUILD_RESOLVED_SOURCE_VERSION}/publish.zip",
    "ApiDns": "api.${AccountDNS}"
}
EOF