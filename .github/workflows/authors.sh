# !/bin/bash

# Checks all commits on the current branch for AI-generated commits and reports a GitHub action error if any are found.

echo "ref: $GITHUB_SHA"
echo "base ref: $GITHUB_BASE_REF"

range="origin/$GITHUB_BASE_REF..$GITHUB_SHA"

echo "Checking commits:"
git log --pretty=reference $range

authors=$(git log --pretty='%ae' $range | sort -u)
coAuthors=$(git log --pretty='format:%B' --grep='Co-authored-by:' $range | grep -i 'Co-authored-by:' | sed -E 's/.* <([^>]+)>/\1/' | sort -u)
allAuthors=$(echo -e "$authors\n$coAuthors" | sort -u)
echo Authors:
echo "$allAuthors"
knownClankers="noreply@anthropic\.com|copilot@github\.com" # TODO: Update this list as needed
clankers=$(echo "$allAuthors" | grep -Ei "$knownClankers")
if [ -n "$clankers" ]; then
    echo "::error title=Clankers detected::Clankers detected in recent commits: $clankers"
    exit 1
fi