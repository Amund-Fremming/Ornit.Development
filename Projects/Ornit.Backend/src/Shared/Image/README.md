# Setup Guide

## Image Processor Client
Create a storage bucketunder R2 Object Storage > Overview > Create bucket
Get your account id under Workers & Pages > Account ID (right side)
Then we need to create a token, så on the same paage under account id press Manage Tokens > Create Token > Create Custom Token
Give the token a name, and add 
1. Create a worker in Cloudflares dashboard under Compute (Workers) > Create > Create Worker
2. Create a js script for receiving your images
```js
export default {
  async fetch(request, env) {
    if (request.method === 'POST') {
      try {
        // Extract the image data from the request body
        const imageData = await request.arrayBuffer();

        // Generate a unique key for the image file
        const key = `image-${Date.now()}.jpg`; // Adjust the filename or extension as needed
        
        // Upload the image data to the R2 bucket and retrieve the response
        await env.MY_BUCKET.put(key, imageData, {
          httpMetadata: {
            contentType: 'image/jpeg', // Specify the content type of the image
          },
        });

        // Manually construct the URL
        const imageUrl = `https://<YOUR_PUBLIC_URL>/${key}`;
        // Respond with the URL of the uploaded image
        return new Response(imageUrl, { status: 200 });
      } catch (error) {
        // Respond with an error message if uploading fails
        return new Response(`Error uploading image: ${error}`, { status: 500 });
      }
    } else {
      // Respond with a "Method Not Allowed" error for non-POST requests
      return new Response('Method Not Allowed', { status: 405 });
    }
  },
};
```
3. Deploy your worker
4. Create a bucket under R2 Object Storage > Create Bucket > Create Bucket
5. Navigate to the bucket > settings > and copy the configuration values to you appsettings like: name, S3 API and public R2.dev Bucket URL.
6. TODO : hvordan få tak accesKey, accountId og secretKey