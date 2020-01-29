pipeline {
    agent any
    environment {
        REPO = "distanceed/distanceedregistration"
        PRIVATE_REPO = "${PRIVATE_DOCKER_REGISTRY}/${REPO}"
        PRIVATE_REPO_EMR = "${PRIVATE_DOCKER_REGISTRY}/${REPO}-emailrunner"
        TAG = "${BUILD_TIMESTAMP}"
    }
    stages {
        stage('Git clone') {
            steps {
                git branch: 'master',
                    url: 'https://sourcecode.lskysd.ca/PublicCode/DistanceEdRegistrationForm.git'
            }
        }
        stage('Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
                    args '-v ${PWD}:/app'
                }
            }
            steps {
                git branch: 'master',
                    url: 'https://sourcecode.lskysd.ca/PublicCode/DistanceEdRegistrationForm.git'

                dir("LSSDDistanceEdReg"){
                    sh 'dotnet build'
                    sh 'dotnet test'
                }
            }
        }
        stage('Docker build') {
            steps {
                dir("LSSDDistanceEdReg"){
                    sh "docker build --no-cache -f Dockerfile-WebFrontEnd -t ${PRIVATE_REPO}:latest -t ${PRIVATE_REPO}:${TAG} ."
                    sh "docker build --no-cache -f Dockerfile-EmailRunner -t ${PRIVATE_REPO}:latest -t ${PRIVATE_REPO}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO}:${TAG}"
                sh "docker push ${PRIVATE_REPO}:latest"
                sh "docker push ${PRIVATE_REPO_EMR}:${TAG}"
                sh "docker push ${PRIVATE_REPO_EMR}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}