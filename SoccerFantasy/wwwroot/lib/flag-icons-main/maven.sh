version=$(git describe --tags --abbrev=0)
url="https://www.webjars.org/deploy?webJarType=npm&nameOrUrlish=flag-icons&version=${version#v}"
echo "curl '${url}'"
curl "$url"
